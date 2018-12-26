using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Checkar_webAPI_core.Classes;
using Checkar_webAPI_core.Data;
using Checkar_webAPI_core.Dtos;
using Checkar_webAPI_core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Checkar_webAPI_core.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAnyOrigin")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepo;
        private readonly IAuthRepository _authRepo;
        private readonly IConfiguration _config;

        public AccountsController(IAccountRepository accountRepo, IAuthRepository authRepo, IConfiguration config)
        {
            _accountRepo = accountRepo;
            _authRepo = authRepo;
            _config = config;
        }

        [HttpPost("activation")]
        public async Task<IActionResult> activateUserAccount(userForActivationDto _userForActivationDto)
        {
            UserLog User = await _accountRepo.GetUserFromUserID(_userForActivationDto.USER_ID);

            if (User == null)
            {
                return BadRequest("something went wrong");
            }

            Token tokenService = new Token(_config.GetSection("AppSettings:SecretKey").Value);

            bool isActivationTokenValid = tokenService.ValidateActivationToken(_userForActivationDto.ACTIVATION_TOKEN, _userForActivationDto.USER_ID);

            if (!isActivationTokenValid)
            {
                return BadRequest("activation session is either invalid or expired");
            }

            Confirmationcode activationConfirmationCode = await _accountRepo.GetActivationCode(_userForActivationDto.ACTIVATION_CODE, _userForActivationDto.USER_ID);
            
            if(activationConfirmationCode == null || activationConfirmationCode.ExpiryTime < DateTime.UtcNow)
            {
                return BadRequest("code is either invalid or expired");
            }

            User.Activated = "T";

            bool isCodeRemoved = _accountRepo.RemoveConfirmationCode(activationConfirmationCode);

            if(!isCodeRemoved)
            {
                return BadRequest("Something went wrong");
            }

            await _accountRepo.SaveUserDetails();

            return Ok();
        }


        [HttpPost("mail/activation")]
        public async Task<IActionResult> sendActivationMail(mailForActivationDto _mailForActivationDto)
        {
            _mailForActivationDto.USER_EMAIL = _mailForActivationDto.USER_EMAIL.ToLower();

            UserLog User = await _accountRepo.GetUserFromEmail(_mailForActivationDto.USER_EMAIL);
            
            if(User == null || User.IduserLog != _mailForActivationDto.USER_ID || User.Activated != "F")
            {
                return BadRequest();
            }

            CodeGenerator codeGenerator = new CodeGenerator();
            String activationCode = codeGenerator.ActivationCodeGenerator();

            Confirmationcode _confirmationCode = new Confirmationcode();

            _confirmationCode.ConfirmationCode1 = activationCode;

            _confirmationCode.ConfirmationType = "ACTIVATION_CODE";
            _confirmationCode.GeneratedOn = DateTime.UtcNow;
            _confirmationCode.ExpiryTime = DateTime.UtcNow.AddDays(1);
            _confirmationCode.Used = "F";
            _confirmationCode.UserId = _mailForActivationDto.USER_ID;



            await _accountRepo.StoreActivationCode(_confirmationCode);


            Token tokenGenerator = new Token(_config.GetSection("AppSettings:SecretKey").Value);
            JwtSecurityToken activationToken = tokenGenerator.GenerateActivationToken(_mailForActivationDto.USER_ID);


            // sending activation mail
            Mailer currentMailer = new Mailer(_config.GetSection("AppSettings:MailerEmail").Value, _config.GetSection("AppSettings:MailerPassword").Value);
            currentMailer.sendActivationMail(_mailForActivationDto.USER_EMAIL, new JwtSecurityTokenHandler().WriteToken(activationToken), activationCode, _mailForActivationDto.USER_ID);

            return Ok(new {
                activation_token = new JwtSecurityTokenHandler().WriteToken(activationToken)
            });
        }

        [HttpPost("recover/reset/password")]
        public async Task<IActionResult> changeAccountPassword(newAccountPasswordDto _newAccountPasswordDto)
        {
            _newAccountPasswordDto.RESET_EMAIL = _newAccountPasswordDto.RESET_EMAIL.ToLower();

            Token tokenService = new Token(_config.GetSection("AppSettings:SecretKey").Value);

            bool isResetTokenValid = tokenService.ValidateResetToken(_newAccountPasswordDto.RESET_TOKEN, _newAccountPasswordDto.RESET_EMAIL);

            if (!isResetTokenValid)
            {
                return BadRequest("reset session is not valid");
            }

            UserLog User = await _accountRepo.GetUserFromEmail(_newAccountPasswordDto.RESET_EMAIL);

            if(User == null)
            {
                return BadRequest("reset email is not valid");
            }

            bool isPasswordMatched =  _accountRepo.isPasswordMatched(User, _newAccountPasswordDto.NEW_PASSWORD);
            if(isPasswordMatched)
            {
                return BadRequest("old and new password can't be same");
            }

            bool isPasswordChanged = await _accountRepo.ChangePasswordViaReset(User, _newAccountPasswordDto.NEW_PASSWORD);

            if (!isPasswordChanged)
            {
                return BadRequest("unable to change password right now");
            }

            return Ok();
        }


        [HttpPost("recover/reset/confirm")]
        public IActionResult verifyResetToken(verifyResetTokenDto _verifyResetTokenDto)
        {
            _verifyResetTokenDto.RESET_EMAIL = _verifyResetTokenDto.RESET_EMAIL.ToLower();

            Token tokenService = new Token(_config.GetSection("AppSettings:SecretKey").Value);
            bool isResetTokenValid = tokenService.ValidateResetToken(_verifyResetTokenDto.RESET_TOKEN, _verifyResetTokenDto.RESET_EMAIL);

            if (!isResetTokenValid)
            {
                return BadRequest("not allowed to reset");
            }

            return Ok();
        }


        [HttpPost("recover/confirm")]
        public async Task<IActionResult> recoveryConfirmation(recoveryConfirmationDto _recoveryConfirmationDto)
        {
            _recoveryConfirmationDto.RECOVERY_EMAIL = _recoveryConfirmationDto.RECOVERY_EMAIL.ToLower();

            bool userExists = await _authRepo.UserExists(_recoveryConfirmationDto.RECOVERY_EMAIL);
            if (!userExists)
            {
               return BadRequest("account does not exist");
            }

            Token tokenService = new Token(_config.GetSection("AppSettings:SecretKey").Value);
            bool isTokenValid = tokenService.ValidateRecoveryToken(_recoveryConfirmationDto.RECOVERY_TOKEN, _recoveryConfirmationDto.RECOVERY_EMAIL);
            if (!isTokenValid)
            {
               return BadRequest("recovery session is not valid");
            }

            UserLog User = await _accountRepo.GetUserFromEmail(_recoveryConfirmationDto.RECOVERY_EMAIL);

            Confirmationcode currentConfirmationCode = await _accountRepo.ConfirmRecoveryCode(_recoveryConfirmationDto.RECOVERY_CODE, User.IduserLog);
            if (currentConfirmationCode == null || currentConfirmationCode.ExpiryTime < DateTime.UtcNow)
            { 
                return BadRequest("code is either expired or invalid");
            }

            JwtSecurityToken reset_token = tokenService.GenerateResetToken(_recoveryConfirmationDto.RECOVERY_EMAIL);

            return Ok(new {
                reset_token = new JwtSecurityTokenHandler().WriteToken(reset_token)
            });

        }

        [HttpPost("recover")]
        public async Task<IActionResult> recover(accountForRecoveryDto _accountForRecoveryDto)
        {
            _accountForRecoveryDto.RECOVERY_EMAIL = _accountForRecoveryDto.RECOVERY_EMAIL.ToLower();


            UserLog User = await _accountRepo.GetUserFromEmail(_accountForRecoveryDto.RECOVERY_EMAIL);

            if(User == null)
            {
                return BadRequest("Unable to find any account associated with this email");
            }


            JwtSecurityToken recoveryToken = new JwtSecurityToken();

            recoveryToken = new Token(_config.GetSection("AppSettings:SecretKey").Value)
                .GenerateRecoveryToken(_accountForRecoveryDto.RECOVERY_EMAIL);

            CodeGenerator codeGenerator = new CodeGenerator();
            string recoveryCode = codeGenerator.RecoveryCodeGenerator();

            var codeToSave = new Confirmationcode()
            {
                ConfirmationCode1 = recoveryCode,
                ConfirmationType = "RECOVERY_CODE",
                GeneratedOn = DateTime.UtcNow,
                ExpiryTime = DateTime.UtcNow.AddDays(1),
                Used = "F",
                UserId = User.IduserLog
            };

            _accountRepo.StoreCode(codeToSave);

            Mailer mailer = new Mailer(_config.GetSection("AppSettings:MailerEmail").Value, _config.GetSection("AppSettings:MailerPassword").Value);
            mailer.sendRecoveryMail(_accountForRecoveryDto.RECOVERY_EMAIL, new JwtSecurityTokenHandler().WriteToken(recoveryToken), recoveryCode);


            return Ok(new
            {
                recovery_token = new JwtSecurityTokenHandler().WriteToken(recoveryToken)
            });
        }

        [Authorize]
        [HttpGet("{userID}/Profile_details")]
        public async Task<IActionResult> Profile_details(int userId)
        {

            int current_login_Userid = int.Parse(User.FindFirst(JwtRegisteredClaimNames.Sid).Value);

            UserProfileDetailsDto temp= await _accountRepo.Userprofile_details(userId,current_login_Userid);
            if(temp.Disabled=="T")
            {
                return BadRequest("Acount is disabled for that user");

            }
            return Ok(temp);
        }


        }
}
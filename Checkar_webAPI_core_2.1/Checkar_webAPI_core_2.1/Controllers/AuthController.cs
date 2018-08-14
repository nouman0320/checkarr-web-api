using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkar_webAPI_core.Data;
using Checkar_webAPI_core.Dtos;
using Checkar_webAPI_core.Model;
using Microsoft.AspNetCore.Mvc;
using Checkar_webAPI_core.Classes;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;

namespace Checkar_webAPI_core.Controllers
{

    [Route("api/[controller]")]
    [EnableCors("AllowAnyOrigin")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IAccountRepository _accountRepo;

        public AuthController(IAuthRepository repo, IConfiguration config, IAccountRepository accountRepo)
        {
            _repo = repo;
            _config = config;
            _accountRepo = accountRepo;
        }


        [HttpPost("token/access/refresh")]
        public async Task<IActionResult> refreshAccessToken(refreshAccessTokenDto _refreshAccessTokenDto)
        {
            _refreshAccessTokenDto.email = _refreshAccessTokenDto.email.ToLower();

            Token tokenService = new Token(_config.GetSection("AppSettings:SecretKey").Value);

            bool isRefreshTokenValid = tokenService.ValidateRefreshToken(_refreshAccessTokenDto.refresh_token, _refreshAccessTokenDto.email);

            if(!isRefreshTokenValid)
            {
                return BadRequest("false");
            }

            string new_refresh_token_ = new JwtSecurityTokenHandler().WriteToken(tokenService.GenerateRefreshToken(_refreshAccessTokenDto.email));
            string new_access_token_ = new JwtSecurityTokenHandler().WriteToken(tokenService.GenerateToken(_refreshAccessTokenDto.email));

            UserLog User = await _accountRepo.GetUserFromEmail(_refreshAccessTokenDto.email);
            if(User == null)
            {
                return BadRequest();
            }


            bool activated = false;
            string activation_check_ = User.Activated;
            if (activation_check_ == "T") activated = true;

            int user_id_ = User.IduserLog;
            string user_email_ = User.UserEmaill;


            return Ok(new
            {
                account_activated = activated,
                user_id = user_id_,
                user_email = user_email_,
                new_refresh_token = new_refresh_token_,
                new_access_token = new_access_token_

            });

        }

        [HttpPost("token/access/validate")]
        public async Task<IActionResult> validateAccessToken(validateAccessTokenDto _validateAcessTokenDto)
        {
            _validateAcessTokenDto.Email = _validateAcessTokenDto.Email.ToLower();

            Token tokenService = new Token(_config.GetSection("AppSettings:SecretKey").Value);

            bool isAccessTokenValid = tokenService.ValidateToken(_validateAcessTokenDto.AccessToken, _validateAcessTokenDto.Email);

            if (!isAccessTokenValid)
            {
                return BadRequest("false");
            }

            UserLog User = await _accountRepo.GetUserFromEmail(_validateAcessTokenDto.Email);

            if(User == null)
            {
                return Unauthorized();
            }

            bool isAccountActivated = false;
            string activation_check = User.Activated;

            if (activation_check == "T") isAccountActivated = true;
            else isAccountActivated = false; ;


            int user_id_ = User.IduserLog;
            

            string user_email_ = User.UserEmaill;


            return Ok(new {
                access_validation = isAccessTokenValid,
                account_activated = isAccountActivated,
                user_id = user_id_,
                user_email = user_email_
            });

        }
        

        [HttpPost("register")]
        public async Task<IActionResult> Register(userForRegisterDto _userForRegisterDto)
        {
             
            _userForRegisterDto.Email = _userForRegisterDto.Email.ToLower();

            if (await _repo.UserExists(_userForRegisterDto.Email))
                return BadRequest("This email is already associated with other account");

            var userToCreate = new UserLog()
            {
                UserEmaill = _userForRegisterDto.Email,
                UserSex = _userForRegisterDto.Sex,
                UserFullname = _userForRegisterDto.Fullname
            };

            var createdUser = await _repo.Register(userToCreate, _userForRegisterDto.Password);

            new Mailer(_config.GetSection("AppSettings:MailerEmail").Value, _config.GetSection("AppSettings:MailerPassword").Value)
                .sendWelcomeMail(createdUser.UserEmaill);

            return StatusCode(201); // have to change this later

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(userForLoginDto _userForLoginDto)
        {
            _userForLoginDto.Email = _userForLoginDto.Email.ToLower();

            UserLog userFromRepo = await _repo.Login(_userForLoginDto.Email, _userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();

            var AccessToken = new Token(_config.GetSection("AppSettings:SecretKey").Value).GenerateToken(userFromRepo.UserEmaill);
            var RefreshToken = new Token(_config.GetSection("AppSettings:SecretKey").Value).GenerateRefreshToken(userFromRepo.UserEmaill);


            return Ok(new
            {
                issued = true,
                token = new JwtSecurityTokenHandler().WriteToken(AccessToken),
                refresh_token = new JwtSecurityTokenHandler().WriteToken(RefreshToken),
                activation_status = userFromRepo.Activated,
                user_id = userFromRepo.IduserLog,
                user_email = userFromRepo.UserEmaill
            });

        }
    }
}
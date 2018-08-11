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

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }


        

        [HttpPost("register")]
        public async Task<IActionResult> Register(userForRegisterDto _userForRegisterDto)
        {
             
            _userForRegisterDto.Email = _userForRegisterDto.Email.ToLower();

            if (await _repo.UserExists(_userForRegisterDto.Email))
                return BadRequest("We're sorry ,this login email already exists");

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

            return Ok(new
            {
                issued = true,
                token = new JwtSecurityTokenHandler().WriteToken(AccessToken),
                refresh_token = "",
                activation_status = userFromRepo.Activated,
                user_id = userFromRepo.IduserLog,
                user_email = userFromRepo.UserEmaill
            });

        }
    }
}
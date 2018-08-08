using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkar_webAPI_core.Data;
using Checkar_webAPI_core.Dtos;
using Checkar_webAPI_core.Model;
using Microsoft.AspNetCore.Mvc;

namespace Checkar_webAPI_core.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }


        

        [HttpPost("register")]
        public async Task<IActionResult> Register(userForRegisterDto _userForRegisterDto)
        {
            _userForRegisterDto.Email = _userForRegisterDto.Email.ToLower();

            if (await _repo.UserExists(_userForRegisterDto.Email))
                return BadRequest("User already exists");

            var userToCreate = new UserLog()
            {
                UserEmaill = _userForRegisterDto.Email,
                UserSex = _userForRegisterDto.Sex,
                UserFullname = _userForRegisterDto.Fullname
            };

            var createdUser = await _repo.Register(userToCreate, _userForRegisterDto.Password);

            return StatusCode(201); // have to change this later

        }
    }
}
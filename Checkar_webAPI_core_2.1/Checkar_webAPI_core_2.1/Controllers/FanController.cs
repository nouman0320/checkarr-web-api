using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Checkar_webAPI_core.Data;
using Checkar_webAPI_core.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Checkar_webAPI_core_2._1.Controllers
{    
   [Authorize]
    [Route("api/[controller]/{userID}")]
    [EnableCors("AllowAnyOrigin")]
    [ApiController]
    public class FanController : ControllerBase
    {
        private readonly IFanRepository _fanRepo;

        public FanController(IFanRepository FanRepo)
        {
            _fanRepo = FanRepo;
        }

        [HttpPost("fan/add")]
        public async Task<IActionResult> AddFan(int userID,AddFanDto _AddFan)
        {
            if (userID != int.Parse(User.FindFirst(JwtRegisteredClaimNames.Sid).Value))
                return Unauthorized();

            bool temp = await _fanRepo.AddFan(userID, _AddFan.FanID);
            if (temp != true)
            {
                return BadRequest("There is something wrong with Addfan");


            }
            return Ok();
        }
        [HttpGet("fan/find")]
        public async Task<IActionResult> FindFan(int userID)
        {
            if (userID != int.Parse(User.FindFirst(JwtRegisteredClaimNames.Sid).Value))
                return Unauthorized();

            List<FindFanForReturnDto>  temp= await _fanRepo.FindFanReturn(userID);



            return Ok(temp);
        }




    }
}

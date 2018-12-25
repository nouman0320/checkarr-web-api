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

    public class PostController : ControllerBase
    {

        private readonly IPostRepository _PostRepo;

        public PostController(IPostRepository postRepo)
        {
            _PostRepo = postRepo;
        }

        [HttpPost("post/add")]
        public async Task<IActionResult> AddPost(int userID, AddPostDto _AddPost)
        {
            if (userID != int.Parse(User.FindFirst(JwtRegisteredClaimNames.Sid).Value))
                return Unauthorized();

            bool temp = await _PostRepo.AddPost(userID, _AddPost.Text_message, _AddPost.Picture_url, _AddPost.Video_url, _AddPost.catagory);
            if (temp != true)
            {
                return BadRequest("There is something wrong with Add Post");


            }
            return Ok();
        }



    }
}

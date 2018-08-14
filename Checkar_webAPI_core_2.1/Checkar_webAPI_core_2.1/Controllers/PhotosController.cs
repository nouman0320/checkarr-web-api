using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Checkar_webAPI_core.Data;
using Checkar_webAPI_core.Dtos;
using Checkar_webAPI_core.Helpers;
using Checkar_webAPI_core.Model;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Checkar_webAPI_core.Controllers
{
    [Authorize]
    [Route("api/[controller]/{userID}")]
    [EnableCors("AllowAnyOrigin")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoRepository _photoRepo;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController(IPhotoRepository photoRepo, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _photoRepo = photoRepo;
            _cloudinaryConfig = cloudinaryConfig;


            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpPost("dp/add")]
        public IActionResult AddDpForUser(int userID)
        {
            if (userID != int.Parse(User.FindFirst(JwtRegisteredClaimNames.Sid).Value))
                return Unauthorized();
            return Ok();
        }
    }
}
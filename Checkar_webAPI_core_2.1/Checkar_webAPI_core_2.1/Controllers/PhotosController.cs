using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Checkar_webAPI_core.Data;
using Checkar_webAPI_core.Dtos;
using Checkar_webAPI_core.Helpers;
using Checkar_webAPI_core.Model;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
        private readonly IMapper _mapper;
        private readonly IPhotoRepository _photoRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController(IMapper mapper ,IPhotoRepository photoRepo, IAccountRepository accountRepo, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _mapper = mapper;
            _photoRepo = photoRepo;
            _accountRepo = accountRepo;
            _cloudinaryConfig = cloudinaryConfig;


            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _photoRepo.GetPhoto(id);

            var photo = _mapper.Map<DpForReturnDto>(photoFromRepo);

            return Ok(photo);
        }

        [HttpPost("dp/add")]
        public async Task<IActionResult> AddDpForUser(int userID, [FromForm]DpForUploadDto _dpForUploadDto)
        {
            if (userID != int.Parse(User.FindFirst(JwtRegisteredClaimNames.Sid).Value))
                return Unauthorized();


            var userFromRepo = await _accountRepo.GetUserFromUserID(userID);

            if (userFromRepo == null)
                return BadRequest("Something went wrong");

            var file = _dpForUploadDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500)
                    };


                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            _dpForUploadDto.Url = uploadResult.Uri.ToString();
            _dpForUploadDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<DisplayPicture>(_dpForUploadDto);
            photo.Active = "T";


            var currentPhoto = await _photoRepo.GetDisplayPictureFromUserID(userID);

            if (currentPhoto != null)
            {
                currentPhoto.Active = "F";

                if (!await _photoRepo.SaveAll())
                {
                    return BadRequest("Something went wrong");
                }
            }
            // check here -> deactive other photos and active main photo

            userFromRepo.DisplayPicture.Add(photo);

            

            if (await _accountRepo.SaveUserDetails())
            {
                var photoToReturn = _mapper.Map<DpForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoToReturn);
            }

            return BadRequest("Could not add photo");
        }
    }
}
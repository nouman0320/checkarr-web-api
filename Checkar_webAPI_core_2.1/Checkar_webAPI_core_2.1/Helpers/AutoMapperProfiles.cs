using AutoMapper;
using Checkar_webAPI_core.Dtos;
using Checkar_webAPI_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<DisplayPicture, DpForReturnDto>();
            CreateMap<DpForUploadDto, DisplayPicture>();
        }
    }
}

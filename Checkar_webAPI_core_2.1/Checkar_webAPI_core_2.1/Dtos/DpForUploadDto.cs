using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Dtos
{
    public class DpForUploadDto
    {
        public string URL { get; set; }
        [Required(ErrorMessage = "Photo is requied to upload")]
        public IFormFile File { get; set; }
        public DateTime CreationDate { get; set; }
        public string PublicId { get; set; }


        public DpForUploadDto()
        {
            CreationDate = DateTime.UtcNow;
        }
    }
}

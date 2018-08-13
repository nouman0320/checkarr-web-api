using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Dtos
{
    public class refreshAccessTokenDto
    {
        [Required(ErrorMessage = "Refresh token is required")]
        public string refresh_token { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string email { get; set; }
    }
}

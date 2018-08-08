using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Dtos
{
    public class userForRegisterDto
    {
        
        [Required]
        [StringLength(256, ErrorMessage = "Your name is too long for our system to handle")]
        public string Fullname { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is not in correct format")]
        public string Email { get; set; }

        [Required]
        [StringLength(1, MinimumLength = 1, ErrorMessage = "Sex is not in correct format")]
        public string Sex { get; set; }


        [Required]
        [StringLength(25, MinimumLength = 6, ErrorMessage = "Your password length should be between 6 - 25")]
        public string Password { get; set; }

    }
}

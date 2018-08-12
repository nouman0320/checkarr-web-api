using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Dtos
{
    public class newAccountPasswordDto
    {

        [Required(ErrorMessage = "Reset token is required")]
        public string RESET_TOKEN { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string RESET_EMAIL { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [StringLength(25, MinimumLength = 6, ErrorMessage = "Your password length should be between 6 - 25")]
        public string NEW_PASSWORD { get; set; }

    }
}

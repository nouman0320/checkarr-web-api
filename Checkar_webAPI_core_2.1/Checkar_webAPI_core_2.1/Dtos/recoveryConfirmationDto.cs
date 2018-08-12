using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Dtos
{
    public class recoveryConfirmationDto
    {
        [Required(ErrorMessage = "Recovery code is required")]
        public string RECOVERY_CODE { get; set; }
        [Required(ErrorMessage = "Recovery token is required")]
        public string RECOVERY_TOKEN { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string RECOVERY_EMAIL { get; set; }
    }
}

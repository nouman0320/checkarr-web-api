using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Dtos
{
    public class recoveryConfirmationDto
    {
        [Required]
        public string RECOVERY_CODE { get; set; }
        [Required]
        public string RECOVERY_TOKEN { get; set; }
        [Required]
        [EmailAddress]
        public string RECOVERY_EMAIL { get; set; }
    }
}

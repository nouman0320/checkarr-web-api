using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Dtos
{
    public class accountForRecoveryDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email is not in correct format")]
        public string RECOVERY_EMAIL { get; set; }
    }
}

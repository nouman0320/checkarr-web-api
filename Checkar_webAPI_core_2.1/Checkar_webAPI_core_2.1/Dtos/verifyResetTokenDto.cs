using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Dtos
{
    public class verifyResetTokenDto
    {
        [Required]
        public string RESET_TOKEN { get; set; }
        [Required]
        [EmailAddress]
        public string RESET_EMAIL { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Dtos
{
    public class userForActivationDto
    {
        [Required(ErrorMessage = "user id is required")]
        public int USER_ID { get; set; }
        [Required(ErrorMessage = "activation code is required")]
        public string ACTIVATION_CODE { get; set; }
        [Required(ErrorMessage = "activation token is required")]
        public string ACTIVATION_TOKEN { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Dtos
{
    public class AddFanDto
    {
        [Required(ErrorMessage = "FanID is required")]
        public int FanID { get; set; }


    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Dtos
{
    public class UserProfileDetailsDto
    {
        public string UserFullname { get; set; }
        public string UserSex { get; set; }
        public string DisplayPicture_url { get; set; }
        public   string  UserReg { get; set; }
        public string Disabled { get; set; }
        public int Total_fans { get; set; }
        public Boolean Fan { get; set; }
        public Boolean Following { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Classes
{
    public class User
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }

        User()
        {
            Fullname = "";
            Email = "";
            Password = "";
            Gender = "";
        }

        
    }
}

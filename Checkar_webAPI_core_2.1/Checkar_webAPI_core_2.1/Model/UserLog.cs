﻿using System;
using System.Collections.Generic;

namespace Checkar_webAPI_core.Model
{
    public partial class UserLog
    {
        public UserLog()
        {
            DisplayPicture = new HashSet<DisplayPicture>();
            Politics = new HashSet<Politics>();
        }

        public int IduserLog { get; set; }
        public string UserFullname { get; set; }
        public string UserEmaill { get; set; }
        public string UserSex { get; set; }
        public DateTime UserReg { get; set; }
        public string Activated { get; set; }
        public string Disabled { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public ICollection<DisplayPicture> DisplayPicture { get; set; }
        public ICollection<Politics> Politics { get; set; }
    }
}

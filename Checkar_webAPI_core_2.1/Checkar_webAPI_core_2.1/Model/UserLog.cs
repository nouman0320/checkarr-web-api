using System;
using System.Collections.Generic;

namespace Checkar_webAPI_core.Model
{
    public partial class UserLog
    {
        public int IduserLog { get; set; }
        public string UserFullname { get; set; }
        public string UserEmaill { get; set; }
        public string UserSex { get; set; }
        public DateTime UserReg { get; set; }
        public string UserPassword { get; set; }
        public string Activated { get; set; }
        public string Disabled { get; set; }
    }
}

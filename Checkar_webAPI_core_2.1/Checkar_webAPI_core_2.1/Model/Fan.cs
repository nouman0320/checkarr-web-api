using System;
using System.Collections.Generic;

namespace Checkar_webAPI_core.Model
{
    public partial class Fan
    {
        public int IdFan { get; set; }
        public int UserId { get; set; }
        public DateTime? TimeAdded { get; set; }
        public int FanAutoid { get; set; }
    }
}

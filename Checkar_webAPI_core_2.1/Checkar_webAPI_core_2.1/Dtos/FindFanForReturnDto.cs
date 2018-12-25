using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Dtos
{
    public class FindFanForReturnDto
    {
        public string UserFullname { get; set; }
        public DateTime? Time { get; set; }
        public int IduserLog { get; set; }
        public string Url { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Dtos
{
    public class DpForReturnDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime CreationDate { get; set; }
        public string PublicId { get; set; }
    }
}

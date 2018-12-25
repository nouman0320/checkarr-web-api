using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Dtos
{
    public class AddPostDto
    {
        public String Text_message { get; set; }
        public String Picture_url { get; set; }
        public String Video_url { get; set; }
        public String catagory { get; set; }

    }
}

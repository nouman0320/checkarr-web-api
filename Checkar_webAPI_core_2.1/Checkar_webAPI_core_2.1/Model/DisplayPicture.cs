using System;
using System.Collections.Generic;

namespace Checkar_webAPI_core.Model
{
    public partial class DisplayPicture
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Active { get; set; }
        public string PublicId { get; set; }
        public UserLog User { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Checkar_webAPI_core.Model
{
    public partial class Politics
    {
        public int PoliticsPostId { get; set; }
        public int? UserId { get; set; }
        public DateTime? Time { get; set; }
        public string TextMessage { get; set; }
        public string PictureUrl { get; set; }
        public string VideoUrl { get; set; }

        public UserLog User { get; set; }
    }
}

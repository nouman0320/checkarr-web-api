using System;
using System.Collections.Generic;

namespace Checkar_webAPI_core.Model
{
    public partial class Post
    {
        public int Postid { get; set; }
        public int UserId { get; set; }
        public int? UpVote { get; set; }
        public string Type { get; set; }
        public int? Fktype { get; set; }
        public DateTime? Date { get; set; }
        public int? Views { get; set; }
        public int? Sugcount { get; set; }
    }
}

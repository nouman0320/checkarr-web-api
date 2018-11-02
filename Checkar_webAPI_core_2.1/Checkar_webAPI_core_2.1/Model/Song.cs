using System;
using System.Collections.Generic;

namespace Checkar_webAPI_core.Model
{
    public partial class Song
    {
        public int IdSong { get; set; }
        public string SongName { get; set; }
        public DateTime? SongAdded { get; set; }
        public string Genre { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Checkar_webAPI_core.Model
{
    public partial class Movie
    {
        public int IdMovie { get; set; }
        public float? Rating { get; set; }
        public int? Ups { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Genre { get; set; }
    }
}

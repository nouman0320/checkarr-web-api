using System;
using System.Collections.Generic;

namespace Checkar_webAPI_core.Model
{
    public partial class Fashion
    {
        public int IdFashion { get; set; }
        public string Type { get; set; }
        public double? Price { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
    }
}

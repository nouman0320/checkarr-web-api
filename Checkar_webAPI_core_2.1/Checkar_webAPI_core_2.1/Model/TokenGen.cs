using System;
using System.Collections.Generic;

namespace Checkar_webAPI_core.Model
{
    public partial class TokenGen
    {
        public int Idtoken { get; set; }
        public DateTime ExpiryTime { get; set; }
        public string TokenType { get; set; }
        public int UserId { get; set; }
        public string TokenString { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Checkar_webAPI_core.Model
{
    public partial class Confirmationcode
    {
        public string ConfirmationCode1 { get; set; }
        public string ConfirmationType { get; set; }
        public string Used { get; set; }
        public int? UserId { get; set; }
        public DateTime? GeneratedOn { get; set; }
        public DateTime? ExpiryTime { get; set; }
        public int CId { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Checkar_webAPI_core.checkarr
{
    public partial class Confirmationcode
    {
        public string ConfirmationCode { get; set; }
        public string ConfirmationType { get; set; }
        public sbyte? Used { get; set; }
        public int? UserId { get; set; }
        public DateTime? GeneratedOn { get; set; }
        public DateTime? ExpiryTime { get; set; }
        public int CId { get; set; }
    }
}

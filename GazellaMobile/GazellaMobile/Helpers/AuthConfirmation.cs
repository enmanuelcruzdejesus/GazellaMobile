using System;
using System.Collections.Generic;
using System.Linq;

namespace GazellaMobile.Helpers
{
    public class AuthConfirmation
    {
        public string UserId { get; set; }
        public int AuthId { get; set; }
        public bool Accept { get; set; }
        public string Comments { get; set; }
    }
}
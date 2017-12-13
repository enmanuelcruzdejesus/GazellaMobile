using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazellaMobile.Utils.Services
{
    public class LoginStatus
    {
        public bool IsValid { get; set; }
        public string Message { get; set; }
       

        public LoginStatus(bool isvalid, string message)
        {
            this.IsValid = isvalid;
            this.Message = message;
         
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
namespace GazellaMobileApi.Models
{    
    public class Authorizations
    {
        public int AutId { get; set; }
        public int Cia { get; set; }
        public int Transaccion { get; set; }
        public int AuthType { get; set; }
        public string Comments { get; set; }
        public int Status { get; set; }        
        public string RequestBy { get; set; }
        public DateTime RequestDate { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovalDate { get; set; }
        public string ApprovalComments { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GazellaMobile.Models;

namespace GazellaMobile.ViewModels
{
    public class AuthorizationDetailsViewModel
    {

        dynamic _auth;
       
        public AuthorizationDetailsViewModel(dynamic authorization)
        {
            _auth = authorization;
        }
        
        

        public int AuthId
        {
            get
            {
                return Convert.ToInt32(_auth.AuthId);

            }

        }

        public string CompanyName
        {
            get
            {
                return _auth.CompanyName.ToString();
                
            }
        }

        public string Description
        {
            get
            {
                return _auth.Description.ToString();
                
            }
        }

        public string RequestDate
        {
            get
            {
                return _auth.RequestDate.ToString();
               
            }
        }

        public string RequestBy
        {
            get
            {
                return _auth.RequestBy.ToString();
                
            }
        }

        public string Status
        {
            get
            {
                return _auth.Status.ToString();
               
            }
        }
       

        public string DescripStatus
        {
            get
            {
                return _auth.DescripStatus.ToString();

            }
        }
        
    }
}

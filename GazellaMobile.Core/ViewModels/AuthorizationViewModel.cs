
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GazellaMobile.Models;
using Newtonsoft.Json;

namespace GazellaMobile.ViewModels
{
    class AuthorizationViewModel
    {
        public Task<dynamic[]> Data
        {
            get
            {
                return App.ServiceClient.GetAuthorizations();                
               
            }
        }
       
        public AuthorizationViewModel()
        {
          
        }

      
      
    }
}

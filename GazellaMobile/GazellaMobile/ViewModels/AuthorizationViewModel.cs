
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GazellaMobileApi.Models;
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
                return GetAuthorizations();
               
            }
        }
        public AuthorizationViewModel()
        {
          
        }

      
        private static async Task<dynamic[]> GetAuthorizations()
        {
            var response = await App.ServiceClient.GetResponse("Authorizations", App.CurrentUser.UserId);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Dictionary<string, List<object>>>(content);
                int records = data["CompanyName"].Count;

                dynamic[] dynamicData = new dynamic[records];


                for (int i = 0; i < records; i++)
                {
                    dynamicData[i] = new
                    {
                        AuthId = data["AuthId"][i],
                        Cia = data["Cia"][i],
                        CompanyName = data["CompanyName"][i],
                        AuthType = data["AuthType"][i],
                        Description = data["Description"][i],
                        RequestDate = data["RequestDate"][i],
                        RequestBy = data["RequestBy"][i],
                        Status = data["Status"][i]
                    };
                }

                return dynamicData;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                //WHEN THE SERVER RAISE AN EXCEPTION     
                return null;
            }
        }
    }
}

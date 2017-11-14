
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GazellaMobile.Models;
using System.Diagnostics;

namespace GazellaMobile.Helpers
{
    public class DataServiceHelper
    {
        GDSServiceClient _service = null;
        public string Uri { get; }       
        public DataServiceHelper(string uri)
        {
            _service = new GDSServiceClient(uri);
        }

        public async Task<dynamic[]> GetAuthorizations()
        {
            var response = await _service.GetResponse("Authorizations", App.CurrentUser.UserId);
            if (response.StatusCode == HttpStatusCode.OK)
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
                        AuthDetail1 = data["AuthDetail1"][i],
                        AuthDetail2 = data["AuthDetail2"][i],
                        Comments = data["Comments"][i],                     
                        RequestDate = data["RequestDate"][i],
                        RequestBy = data["RequestBy"][i],
                        MoreDetail = data["MoreDetail"][i],
                        Status = data["Status"][i]
                      
                    };
                }

                return dynamicData;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return new dynamic[] { };
            }            
            else
            {
                //WHEN THE SERVER RAISE AN EXCEPTION     
                return null;
            }
        }
        public async Task<Dictionary<string, List<object>>> ExecProcedureData(ProcedureParams p)
        {
            var response = await _service.Post<ProcedureParams>("GDSApi", p);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<Dictionary<string, List<object>>>(content);
                return data; 

            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                // //WHEN THE SERVER RAISE AN EXCEPTION                   
                return null;
            }

            return null;

        }    
        public async Task<string> AuthConfirmationResponse(AuthConfirmation authConfirmation)
        {
            var response = await _service.Post<AuthConfirmation>("Authorizations", authConfirmation);
           
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data  = JsonConvert.DeserializeObject<Dictionary<string, List<object>>>(content);
                var message = data["message"][0].ToString();
                return message;
                
            }
            else if(response.StatusCode == HttpStatusCode.InternalServerError)
            {
                // //WHEN THE SERVER RAISE AN EXCEPTION                   
                var content = response.Content.ReadAsStringAsync().Result;
                dynamic ex = JsonConvert.DeserializeObject(content);
                return ex.message;
            }

            return null;
           
        }
        
    }
}

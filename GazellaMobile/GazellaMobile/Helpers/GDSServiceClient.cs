using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace GazellaMobile.Helpers
{
    public class GDSServiceClient
    {
        private HttpClient _client;
        private string Url { get; set; }

        public GDSServiceClient(string url)
        {
            Url = url;
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Connection", "close");

        }

        public async Task<HttpResponseMessage> GetResponse(string controller)
        {
            var uri = new Uri(String.Format(Url, controller, string.Empty));
            var response = await _client.GetAsync(uri);
            return response;
        }


        public async Task<HttpResponseMessage> GetResponse(string controller, string id)
        {
            var uri = new Uri(String.Format(Url, controller, id));
            var response = await _client.GetAsync(uri);
            return response;
        }

        public async Task<HttpResponseMessage> Post<T>(string controller, T id)
        {
            var uri = new Uri(string.Format(Url, controller, string.Empty));
            var json = JsonConvert.SerializeObject(id);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, content);
            return response;

        }

        public async Task<HttpResponseMessage> Put<T>(string controller,T id)
        {
            var uri = new Uri(string.Format(Url, controller, string.Empty));
            var json = JsonConvert.SerializeObject(id);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(uri, content);
            return response;
        }

     
    }
}


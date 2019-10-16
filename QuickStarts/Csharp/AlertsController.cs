using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MSGraphSecurity
{
    public class AlertsController
    {
        public AlertsController()
        {
            
        }

        public async Task<string> GetAlerts(string content = "")
        {
            var token = await GetToken();

            string url = string.Format("https://graph.microsoft.com/beta/security/alerts/");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            HttpClient http = new HttpClient();

            var response = await http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                object formatted = JsonConvert.DeserializeObject(error);
                throw new WebException("Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented));
            }

            string json = await response.Content.ReadAsStringAsync();

            return json;
        }

        public async Task<string> GetAlert(string id, string content = "")
        {
            var token = await GetToken();

            string url = string.Format("https://graph.microsoft.com/beta/security/alerts/" + id);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            HttpClient http = new HttpClient();

            var response = await http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                object formatted = JsonConvert.DeserializeObject(error);
                throw new WebException("Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented));
            }

            string json = await response.Content.ReadAsStringAsync();

            return json;
        }

        public async Task<string> UpdateAlert(string id, object alert)
        {
            var token = await GetToken();

            string url = string.Format("https://graph.microsoft.com/beta/security/alerts/" + id);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var stringAlert = JsonConvert.SerializeObject(alert);

            request.Content = new StringContent(stringAlert, Encoding.UTF8, "application/json");

            HttpClient http = new HttpClient();

            var response = await http.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                object formatted = JsonConvert.DeserializeObject(error);
                throw new WebException("Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented));
            }

            string json = await response.Content.ReadAsStringAsync();

            return json;
        }

        private async Task<AuthenticationResult> GetToken()
        {
            var accessToken = new AccessToken();

            var token = await accessToken.GetToken();

            return token;
        }
    }
}

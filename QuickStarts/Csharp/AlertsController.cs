using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
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

        //create GET request to retrieve all security alerts
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

        //create GET request to retrieve a single security alert by Id
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

        //create PATCH request to update a single security alert by Id
        public async Task<string> PatchAlert(string id, Alert alert)
        {
            var token = await GetToken();

            string url = string.Format("https://graph.microsoft.com/beta/security/alerts/" + id);

            //vendorInformation is required by the API for patching alerts,
            //so first we need to make a call to GET the alert.
            var oldAlert = await GetAlert(id);
            //convert oldAlert to Json object
            var alertJson = JsonConvert.DeserializeObject<Alert>(oldAlert);

            //then add vendorInformation to the PATCH body
            alert.VendorInformation = alertJson.VendorInformation;

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

        //create GET request to retrieve access token
        private async Task<AuthenticationResult> GetToken()
        {
            var accessToken = new AccessToken();

            var token = await accessToken.GetToken();

            return token;
        }
    }
}

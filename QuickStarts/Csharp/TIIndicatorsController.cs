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
    public class TIIndicatorsController
    {
        public TIIndicatorsController()
        {

        }

        #region Get All. Get, Create, Update, Delete One TI Indicator
        //create GET request to retrieve all TI Indicators
        public async Task<string> GetTIIndicators(string content = "")
        {
            var token = await GetToken();

            string url = string.Format("https://graph.microsoft.com/beta/security/tiIndicators");

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

        //create GET request to retrieve a single TI Indicator by Id
        public async Task<string> GetTIIndicator(string id, string content = "")
        {
            var token = await GetToken();

            string url = string.Format("https://graph.microsoft.com/beta/security/tiIndicators/" + id);

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

        //create POST request to submit a single TI Indicator
        public async Task<string> CreateTIIndicator(TiIndicator tiIndicator)
        {
            var token = await GetToken();

            string url = string.Format("https://graph.microsoft.com/beta/security/tiIndicators");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var stringTIIndicator = JsonConvert.SerializeObject(tiIndicator);

            request.Content = new StringContent(stringTIIndicator, Encoding.UTF8, "application/json");

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

        //create PATCH request to update a single TI Indicator by Id
        public async Task<string> UpdateTIIndicator(string id, TiIndicator tIIndicator)
        {
            var token = await GetToken();

            string url = string.Format("https://graph.microsoft.com/beta/security/tiIndicators/" + id);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var stringTIIndicator = JsonConvert.SerializeObject(tIIndicator);

            request.Content = new StringContent(stringTIIndicator, Encoding.UTF8, "application/json");

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

        //create DELETE request to remove a single TI Indicator by Id
        public async Task<string> DeleteTIIndicator(string id, string content = "")
        {
            var token = await GetToken();

            string url = string.Format("https://graph.microsoft.com/beta/security/tiIndicators/" + id);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);

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

        #endregion

        #region Create, Update, Delete Multiple TI Indicators

        //create POST request to submit multiple TI Indicators
        public async Task<string> CreateMultipleTIIndicators(List<TiIndicator> indicators)
        {
            var token = await GetToken();

            string url = string.Format("https://graph.microsoft.com/beta/security/tiIndicators/submitTiIndicators");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var obj = new { value = indicators };

            var value = JsonConvert.SerializeObject(obj);

            request.Content = new StringContent(value, Encoding.UTF8, "application/json");

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

        //create POST request to update multiple TI Indicators
        public async Task<string> UpdateMultipleTIIndicators(List<TiIndicator> indicators)
        {
            var token = await GetToken();

            string url = string.Format("https://graph.microsoft.com/beta/security/tiIndicators/updateTiIndicators");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var obj = new { value = indicators };
 
            var value = JsonConvert.SerializeObject(obj);

            request.Content = new StringContent(value, Encoding.UTF8, "application/json");

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

        //create POST request to delete multiple TI Indicators
        public async Task<string> DeleteMultipleTIIndicators(List<string> tiList)
        {
            var token = await GetToken();

            string url = string.Format("https://graph.microsoft.com/beta/security/tiIndicators/deleteTiIndicators");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var obj = new { value = tiList };

            var value = JsonConvert.SerializeObject(obj);

            request.Content = new StringContent(value, Encoding.UTF8, "application/json");

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

        //create POST request to delete multiple TI Indicators by external IDs
        public async Task<string> DeleteTiIndicatorsByExternalId(List<string> externalIDList)
        {
            var token = await GetToken();

            string url = string.Format("https://graph.microsoft.com/beta/security/tiIndicators/deleteTiIndicatorsByExternalId");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var obj = new { value = externalIDList };

            var value = JsonConvert.SerializeObject(obj);

            request.Content = new StringContent(value, Encoding.UTF8, "application/json");

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

        #endregion

        //create GET request to retrieve access token
        private async Task<AuthenticationResult> GetToken()
        {
            var accessToken = new AccessToken();

            var token = await accessToken.GetToken();

            return token;
        }
    }
}

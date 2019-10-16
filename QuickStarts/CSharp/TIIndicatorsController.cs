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

        #region Get, Creat, Update, Delete One TI Indicator
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

        public async Task<string> CreateTIIndicator(object tiIndicator)
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

        public async Task<string> UpdateTIIndicator(string id, object tIIndicator)
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

        #region Create, Update, Delete Multiple TI Indicators - This part is currently being worked on
        public async Task<string> CreateMultipleTIIndicators(List<TiIndicator> value)
        {
            try
            {
                var token = await GetToken();

                //string url = string.Format("https://graph.microsoft.com/beta/security/tiIndicators/");
                string url = string.Format("https://graph.microsoft.com/beta/security/tiIndicators/submitTiIndicators");

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

                var stringTIIndicator = JsonConvert.SerializeObject(value);

                request.Content = new StringContent(stringTIIndicator, Encoding.UTF8, "application/json");

                //request.Content = value;

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

            //var jsonList = new List<string>();

            //foreach (var tiIndicator in tiIndicators)
            //{
            //    var stringTiIndicator = JsonConvert.SerializeObject(tiIndicator);

            //    request.Content = new StringContent(stringTiIndicator, Encoding.UTF8, "application/json");

            //    HttpClient http = new HttpClient();

            //    var response = await http.SendAsync(request);

            //    if (!response.IsSuccessStatusCode)
            //    {
            //        string error = await response.Content.ReadAsStringAsync();
            //        object formatted = JsonConvert.DeserializeObject(error);
            //        throw new WebException("Error Calling the Graph API: \n" + JsonConvert.SerializeObject(formatted, Formatting.Indented));
            //    }

            //    string json = await response.Content.ReadAsStringAsync();

            //    jsonList.Add(json);
            //}


        #endregion

        private async Task<AuthenticationResult> GetToken()
        {
            var accessToken = new AccessToken();

            var token = await accessToken.GetToken();

            return token;
        }
    }
}

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MSGraphSecurity
{
    public class SecureScoreControlProfilesController
    {
        public SecureScoreControlProfilesController()
        {

        }

        //create GET request to retrieve all secure score control profiles
        public async Task<string> Get(string content = "")
        {
            var token = await GetToken();

            string url = string.Format("https://graph.microsoft.com/beta/security/secureScoreControlProfiles");

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

        //create PATCH request to update a single secure score control profile by Id
        public async Task<string> Patch(string id, SecureScoreControlProfile profile)
        {
            var token = await GetToken();

            string url = string.Format("https://graph.microsoft.com/beta/security/secureScoreControlProfiles" + id);

            //vendorInformation is required by the API for patching secure score control profiles,
            //so first we need to make a call to GET the profile.
            var oldProfile = await Get(id);
            //convert oldProfile to Json object
            var profileJson = JsonConvert.DeserializeObject<SecureScoreControlProfile>(oldProfile);

            //then add vendorInformation to the PATCH body
            profile.VendorInformation = profileJson.VendorInformation;

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Patch, url);

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var stringProfile = JsonConvert.SerializeObject(profile);

            request.Content = new StringContent(stringProfile, Encoding.UTF8, "application/json");

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

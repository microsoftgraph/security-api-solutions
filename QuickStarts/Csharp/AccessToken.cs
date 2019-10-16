using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSGraphSecurity
{
    public class AccessToken
    {
        static ClientCredential credential;
        static AuthenticationContext authContext;

        static readonly string tenantId = "enter your tenantID";
        static readonly string appId = "enter your appID";
        static readonly string appSecret = "enter your app secret";

        public AccessToken()
        {

        }

        public async Task<AuthenticationResult> GetToken()
        {
            authContext = new AuthenticationContext("https://login.microsoftonline.com/" + tenantId);

            credential = new ClientCredential(appId, appSecret);

            var GraphAAD_URL = string.Format("https://graph.microsoft.com/");

            try
            {
                AuthenticationResult result = await authContext.AcquireTokenAsync(GraphAAD_URL, credential);

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Acquiring Access Token: \n" + ex.Message);
            }
        }
    }
}

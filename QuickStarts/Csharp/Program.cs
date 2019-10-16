using Microsoft.Graph;
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
    class Program
    { 
        public static async Task Main()
        {
            #region Get Access Token

            var accessToken = new AccessToken();
            var token = await accessToken.GetToken();
            if (token != null)
            {
                Console.WriteLine("Access Token: \n" + token.AccessToken);
                Console.WriteLine("******************************************");
            }

            #endregion

            #region Alerts

            //var alertsController = new AlertsController();
            //var alerts = await alertsController.GetAlerts();
            //Console.WriteLine("List of alerts \n" + alerts);


            //var alertJson = JsonConvert.DeserializeObject(alerts);
            //Console.WriteLine(alertJson);
            //var alertId = alertJson["value"];
            //var alert = await alertsController.GetAlert(alertId);
            //Console.WriteLine("One Alert \n" + alert);

            //var newAlertProperties = new
            //{
            //    vendorInformation = new
            //    {
            //        provider = "Palo Alto Networks",
            //        providerVersion = "8.1",
            //        subProvider = "NGFW",
            //        vendor = "Palo Alto Networks"
            //    },
            //    assignedTo = "Bob Smith",
            //    closedDateTime = DateTime.Now,
            //    comments = new List<string> { "The alert was benign" },
            //    feedback = "falsePositive",
            //    status = "resolved",
            //    tags = new List<string>{ "HVA", "SAW" },
            //};

            //await UpdateAlert(alertId, newAlertProperties);

            //var alert = await GetAlert(alertId);
            //Console.WriteLine("Alert \n \n" + alert);
            //Console.WriteLine("******************************************");

            #endregion

            #region SecureScores & SecureScoreProfiles
            //var secureScores = await GetSecureScores();
            //Console.WriteLine("Secure Scores \n \n" + secureScores);

            //var secureScoreControlPolicies = await GetSecureScoreControlPolicies();
            //Console.WriteLine("Secure Score Control Policies" + secureScoreControlPolicies);
            //Console.WriteLine("******************************************");

            #endregion

            #region Get, Create, Update, and Delete one TI Indicator

            var tiIndicatorsController = new TIIndicatorsController();

            var newTIIndicator = new
            {
                action = "alert",
                description = "Test required fields for each TI",
                expirationDateTime = "2019-10-01T21:43:37.5031462+00:00",
                targetProduct = "Azure Sentinel",
                threatType = "WatchList",
                tlpLevel = "green",
                confidence = 0,
                externalId = "Demo TI--9586509942679764298MS502",
                fileHashType = "sha256",
                fileHashValue = "bb12328647b57bf51524d1756b2ed746e5a3f31b67cf7fe5b5d8a9daf07ca313",
                severity = 0,
                tags = new List<string>()
            };

            await tiIndicatorsController.CreateTIIndicator(newTIIndicator);
            Console.WriteLine("Successully created a threat intelligent indicator");

            //var tiIndicatorId = string.Format("35B9546B1AF0E35A674AA0CF2D67E4213519DAF43B018C63AD8F8A0985FD495E");
            //var tiIndicator = await tiIndicatorsController.GetTIIndicator(tiIndicatorId);
            //Console.WriteLine("One Threat Intelligent Indicator: \n \n" + tiIndicator);
            //Console.WriteLine("******************************************");

            //var newTIFields = new
            //                {
            //                    AdditionalInformation = "additionalInformation-after-update",
            //                    Confidence = 42,
            //                    Description = "description-after-update"
            //                };

            //await tiIndicatorsController.UpdateTIIndicator(tiIndicatorId, newTIFields);
            //Console.WriteLine("Successully updated TI indicator with id {0}", tiIndicatorId);

            //await tiIndicatorsController.DeleteTIIndicator(tiIndicatorId);
            //Console.WriteLine("Successully deleted TI indicator with id {0}", tiIndicatorId);

            #endregion

            #region TODO: Create, Update, Delete Multiple TI Indicators

            var value = new List<TiIndicator>()
            {   new TiIndicator
                {
                    ActivityGroupNames = new List<string>(),
                    Confidence = 0,
                    Description = "TI Indicator 1",
                    ExpirationDateTime = DateTimeOffset.Parse("2019-03-01T21:44:03.1668987+00:00"),
                    ExternalId = "Test--8586509942423126760MS164-0",
                    FileHashType = "Sha256",
                    FileHashValue = "e111c45c5b1b01304217e72118d6ca1b14b7013644a078273cea27bbdc1cf9d6",
                    KillChain = new List<string>(),
                    MalwareFamilyNames = new List<string>(),
                    Severity = 0,
                    Tags = new List<string>(),
                    TargetProduct = "Azure Sentinel",
                    ThreatType = "WatchList",
                    TlpLevel = "Green"
                 },
                new TiIndicator
                {
                    ActivityGroupNames = new List<string>(),
                    Confidence = 0,
                    Description = "Ti Indicator 2",
                    ExpirationDateTime = DateTimeOffset.Parse("2019-03-01T21:44:03.1748779+00:00"),
                    ExternalId = "Test--8586509942423126760MS164-1",
                    FileHashType = "Sha256",
                    FileHashValue = "1234b433950990b28d6a22456c9d2b58ced1bdfcdf5f16f7e39d6b9bdca4213b",
                    KillChain = new List<string>(),
                    MalwareFamilyNames = new List<string>(),
                    Severity = 0,
                    Tags = new List<string>(),
                    TargetProduct = "Azure Sentinel",
                    ThreatType = "WatchList",
                    TlpLevel = "Green"
                }
            };
            //var createTIIndicators = await tiIndicatorsController.CreateMultipleTIIndicators(value);
            //Console.WriteLine(createTIIndicators);

            //var tiIndicators = await tiIndicatorsController.GetTIIndicators();
            //Console.WriteLine("List of Threat Intelligent Indicators: \n \n" + tiIndicators);
            //Console.WriteLine("******************************************");

            #endregion
        }
    }
}
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSGraphSecurity
{
    class Program
    {
        public static async Task Main()
        {

            //Get access token
            Console.WriteLine("Getting access token...");
            var accessToken = new AccessToken();
            var token = await accessToken.GetToken();
            Console.WriteLine("Received access token: \n" + token.AccessToken);
            Console.WriteLine("******************************************");

            //Uncomment the code below to Get all alerts
            //Console.WriteLine("Getting alerts...");
            //var alertsController = new AlertsController();
            //var alerts = await alertsController.GetAlerts();
            //Console.WriteLine("Received alerts: \n" + alerts);
            //Console.WriteLine("******************************************");

            //Uncomment the code below to Get all SecureScores
            //Console.WriteLine("Getting Secure scores...");
            //var secureScoresController = new SecureScoresController();
            //var secureScores = await secureScoresController.Get();
            //if (secureScores != null)
            //    Console.WriteLine("Received Secure Scores: \n" + secureScores);
            //Console.WriteLine("******************************************");

            //Uncomment the code below to Get all SecureScoresControlProfiles
            //Console.WriteLine("Getting Secure Score Control Profiles...");
            //var profileController = new SecureScoreControlProfilesController();
            //var secureScoreControlProfiles = await profileController.Get();
            //Console.WriteLine("Received Secure Score Control Profiles: \n" + secureScoreControlProfiles);
            //Console.WriteLine("******************************************");

            //Uncomment the code below to Get all TI Indicators
            //Console.WriteLine("Getting TI Indicators...");
            //var tiIndicatorsController = new TIIndicatorsController();
            //var tiIndicators = await tiIndicatorsController.GetTIIndicators();
            //Console.WriteLine("Received TI Indicators: \n" + tiIndicators);
            //Console.WriteLine("******************************************");

            //Uncomment the code below to Create a TI Indicator
            //var ti = new TiIndicator
            //{
            //    Action = "alert",
            //    Description = "TI 1",
            //    ExpirationDateTime = DateTimeOffset.Parse("2019-12-31T21:44:03.1668987+00:00"),
            //    ExternalId = "External Id 1",
            //    TargetProduct = "Azure Sentinel",
            //    ThreatType = "WatchList",
            //    TlpLevel = "green",
            //    Url = "http://6.7.8.9"
            //};
            //var postTI = await tiIndicatorsController.CreateTIIndicator(ti);
            //Console.WriteLine("POST TI RESULT:\n" + postTI);
            //Console.WriteLine("******************************************");

            //Uncomment the code below to Create multiple TI Indicators
            //var tiList = new List<TiIndicator>()
            //{
            //    new TiIndicator
            //    {
            //        Action = "alert",
            //        Confidence = 0,
            //        Description = "TI 2",
            //        ExpirationDateTime = DateTimeOffset.Parse("2019-12-31T21:44:03.1668987+00:00"),
            //        ExternalId = "External ID 2",
            //        Severity = 0,
            //        TargetProduct = "Azure Sentinel",
            //        ThreatType = "WatchList",
            //        TlpLevel = "green",
            //        Url = "http://3.4.5.6"
            //    },
            //    new TiIndicator
            //    {
            //        Action = "block",
            //        Confidence = 0,
            //        Description = "TI 3",
            //        ExpirationDateTime = DateTimeOffset.Parse("2019-12-31T21:44:03.1668987+00:00"),
            //        ExternalId = "External ID 3",
            //        Severity = 0,
            //        TargetProduct = "Azure Sentinel",
            //        ThreatType = "WatchList",
            //        TlpLevel = "green",
            //        Url = "http://2.3.4.5"
            //    }
            //};
            //var postTIs = await tiIndicatorsController.CreateMultipleTIIndicators(tiList);
            //Console.WriteLine("POST TI RESULT:\n" + postTIs);
            //Console.WriteLine("******************************************");

            //Uncomment the code below to Update multiple TI indicators
            //var values = new List<TiIndicator>()
            //{   new TiIndicator
            //    {
            //        Id = "<id-value1>",
            //        AdditionalInformation = "my test",
            //    },
            //    new TiIndicator
            //    {
            //        Id = "<id-value2>",
            //        AdditionalInformation = "my test again",
            //    }
            //};
            //var updateTIs = await tiIndicatorsController.UpdateMultipleTIIndicators(values);
            //Console.WriteLine("UPDATE Multiple TI RESULT:\n" + updateTIs);
            //Console.WriteLine("******************************************");

            //Uncomment the code below to Delete multiple TI indicators
            //var idsToDelete = new List<string>()
            //{
            //    "<id-value1>",
            //    "<id-value2>"
            //};
            //var deletedTIs = await tiIndicatorsController.DeleteMultipleTIIndicators(idsToDelete);
            //Console.WriteLine("Delete TI RESULT:\n" + deletedTIs);
            //Console.WriteLine("******************************************");

            //Uncomment the code below to Delete multiple TI indicators by external IDs
            //    var externalIDsToDelete = new List<string>()
            //    {
            //        "<externalId-value1>",
            //        "<externalId-value2>"
            //    };
            //    var result = await tiIndicatorsController.DeleteTiIndicatorsByExternalId(externalIDsToDelete);
            //    Console.WriteLine("Delete by external TI RESULT:\n" + result);
            //    Console.WriteLine("******************************************");
        }
    }
}
# C# QuickStart Sample

## Description
This repository contains a very simple C# console application that implements the Microsoft Graph Security API.

### Examples Provided
- GET all alerts
- GET and PATCH one alert
- GET all secure scores
- GET all secure score control profiles
- PATCH one secure score control profile
- GET all TI Indicators
- GET, PATCH, and DELETE one TI indicator
- CREATE, PATCH, and DELETE multiple TI indicators
- DELETE multiple TI indicators by External IDs
> > - PATCH and DELETE bulk TI indicators currently support only `Azure Sentinel` **targetProduct**.
> > - **targetProduct** and **expirationDateTime** are required for PATCH and DELETE bulk methods.

## App Registration
To configure the sample, you'll need to register a new application in the Microsoft [Application Registration Portal](https://go.microsoft.com/fwlink/?linkid=2083908).
Follow these steps to register a new application:
1. Sign in to the [Application Registration Portal](https://go.microsoft.com/fwlink/?linkid=2083908) using either your personal or work or school account.

2. Choose **New registration**.

3. Enter an application name, and choose **Register**.

4. Next you'll see the overview page for your app. Copy and save the **Application Id** field. You will need it later to complete the configuration process.

5. Under **Certificates & secrets**, choose **New client secret** and add a quick description. A new secret will be displayed in the **Value** column. Copy this password. You will need it later to complete the configuration process and it will not be shown again.

6. Under **API permissions**, choose **Add a permission > Microsoft Graph**.

1. Under **Application Permissions**, add the permissions/scopes required for the sample. This sample requires **ThreatIndicators.ReadWrite.OwnedBy**.
    >Note: See the [Microsoft Graph permissions reference](https://developer.microsoft.com/en-us/graph/docs/concepts/permissions_reference) for more information about Graph's permission model.

As the final step in configuring the script, modify the AccessToken.cs file in the root folder of your cloned repo.

Update the variables in the file as follows:
```
static readonly string tenantId = "<enter your tenant ID>";
static readonly string appId = "<enter your application/client ID>";
static readonly string appSecret = "<enter your application/client secret>";
```
Once changes are complete, save the file. After you've completed these steps and have received [admin consent](https://github.com/microsoftgraph/python-security-rest-sample#Get-Admin-consent-to-view-Security-data) for your app, you'll be able to run the app.js sample as covered below.

## Notes:
* The "Main" function in the Program.cs class is the entry point for the application.
* Uncomment the code to initialize the classes and call the methods

## Instructions
1) Clone this repository
2) Register an application using the steps above
3) Install necessary dependencies
4) Run the application

## Additional Resources
For additional informaiton on using Microsoft Graph or the Microsoft Graph Security API, please visit <https://docs.microsoft.com/graph/api/resources/security-api-overview>

## Copyright (c) Microsoft Corporation. All rights reserved.
## Licensed under the MIT License. 

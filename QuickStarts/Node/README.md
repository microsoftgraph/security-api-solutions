# Node QuickStart Sample

## Description
This repository contains a very simple Node application that implements the Microsoft Graph Security API.

### Examples Provided
- GET all alerts
- GET one alert
- PATCH one alert

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

As the final step in configuring the script, modify the config.py file in the root folder of your cloned repo.

Update the variables in app.js as follows:
```
const appId = '<enter your application/client ID>';
const appSecret = '<enter your application/client secret>';
const tenantId = '<enter your tenant ID>';

```
Once changes are complete, save the file. After you've completed these steps and have received [admin consent](https://github.com/microsoftgraph/python-security-rest-sample#Get-Admin-consent-to-view-Security-data) for your app, you'll be able to run the app.js sample as covered below.

## Instructions
1) Clone this repository
2) Register an application using the steps above
3) Execute `npm i` to install "request" and "request-promise"
3) Execute `node app.js` to run the code

## Additional Resources
For additional informaiton on using Microsoft Graph or the Microsoft Graph Security API, please visit <https://docs.microsoft.com/graph/>

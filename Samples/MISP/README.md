# MISP to Microsoft Security Graph Script
MISP Graph Script provides clients with MISP instances to migrate threat indicators to the Microsoft Security Graph API.  

## Prerequisites
Before installing the sample:
Install Python 3.x version from https://www.python.org/. 
To register your application for access to Microsoft Graph, you'll need either a [Microsoft account](https://www.outlook.com/) or an [Office 365 for business account](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account). If you don't have one of these, you can create a Microsoft account for free at [outlook.com](https://www.outlook.com/).

## Configuration
To configure the samples, you'll need to register a new application in the Microsoft [Application Registration Portal](https://apps.dev.microsoft.com/).
Follow these steps to register a new application:
1. Sign in to the [Application Registration Portal](https://apps.dev.microsoft.com/) using either your personal or work or school account.

1. Under My applications, choose Add an app. If you're using an Office 365 account and see two categories listed (Converged or Azure AD only), choose Add an app for the Converged applications section.

1. Enter an application name, and choose Create. (Do not choose Guided Setup.)

1. Next you'll see the registration page for your app. Copy and save the Application Id field.You will need it later to complete the configuration process.

1. Under Application Secrets, choose Generate New Password. A new password will be displayed in the New password generated dialog. Copy this password. You will need it later to complete the configuration process.

1. Under Platforms, choose Add platform > Web.

1. Under Delegated Permissions, add the permissions/scopes required for the sample. This sample requires User.Read, SecurityEvents.Read.All, and SecurityEvents.ReadWrite.All permissions.
    >Note: See the [Microsoft Graph permissions reference](https://developer.microsoft.com/en-us/graph/docs/concepts/permissions_reference) for more information about Graph's permission model.

1. Enter http://localhost:5000/login/authorized as the Redirect URL, and then choose Save.

Follow these steps to allow [webhooks](https://developer.microsoft.com/en-us/graph/docs/concepts/permissions_reference) to access the sample via a NGROK tunnel:
>Note: This is required if you want to test the sample Notification Listener on localhost. You must expose a public HTTPS endpoint to create a subscription and receive notifications from Microsoft Graph. While testing, you can use ngrok to temporarily allow messages from Microsoft Graph to tunnel to a localhost port on your computer.
1. Download [ngrok](https://ngrok.com/download).

1. Follow the installation instructions on the ngrok website.

1. Run ngrok, if you are using Windows. Run "ngrok.exe http 5000" to start ngrok and open a tunnel to your localhost port 5000.

1. Then update the config.py file with your ngrok url.

As the final step in configuring the script, modify the config.py file in the root folder of your cloned repo.

Update tenent, client_id, and client_secret in config.py
```
graph_auth = {
    'tenant': '<tenant id>',
    'client_id': '<client id>',
    'client_secret': '<client secret>',
}
```
Once changes are complete, save the config file. After you've completed these steps and have received [admin consent](https://github.com/microsoftgraph/python-security-rest-sample#Get-Admin-consent-to-view-Security-data) for your app, you'll be able to run the script.py sample as covered below.

## Additional Configurations
### Target Product
`targetProduct = "Azure Sentinel"`

### Misp Event Filter
This field is needed to filter events from the Misp Instance.

Below is a list of parameters that can be passed to the filter (source: https://pymisp.readthedocs.io/modules.html):
* values – values to search for
* not_values – values not to search for
* type_attribute – Type of attribute
* category – Category to search
* org – Org reporting the event
* tags – Tags to search for
* not_tags – Tags not to search for
* date_from – First date
* date_to – Last date
* last – Last published events (for example 5d or 12h or 30m)
* eventid – Evend ID
* withAttachments – return events with or without the attachments
* uuid – search by uuid
* publish_timestamp – the publish timestamp

A list or a specific value can be passed to the above parameters. If a list is passed to the parameter, the filtered events are the result of the union of provided list.

This field needs to be a list that contains multiple filters. The filtered events are the result of the intersection of provided filters.

#### First Example of How This Field can be Configured
```
misp_event_filters = [
    {
        "type_attribute": 'mutex'
    },
    {
        "type_attribute": 'filename|md5'
    },
]
```
An event meets this filtering criteria if the event has an attribute with attribute type of 'mutex' AND the event has an attribute with attribute type of 'filename|md5'.

#### Second Example of How This Field can be Configured
```
misp_event_filters = [
    {
        "type_attribute": ['mutex', 'filename|md5']
    }
]
```
An event meets this filtering criteria if the event has an attribute with attribute type of 'mutex' OR the event has an attribute with attribute type of 'filename|md5'.

#### Third Example of How This Field can be Configured
```
misp_event_filters = [
    {
        "values": 'http://www.test.com'
    }
]
```
An event meets this filtering criteria if the event has an attribute with attribute value of 'http://www.test.com'.

#### Fourth Example of How This Field can be Configured
```
misp_event_filters = []
```
This gets all events.

### Action
`action = "alert"`

### Passive Only
`passiveOnly = False`

### Days to Expire
This field is needed to specify how long it should take for Threat Indicators pushed to Microsoft Graph API to expire. 

`days_to_expire = 5`

### Misp Key
The Misp Key is required to fetch data from your Misp instance. 
It can be found in the event actions menu under automation on the website of the Misp instance.

`misp_key = '<misp key>'`

## Instructions on How to Run 
1. Install the latest Python 3 (refer to https://www.python.org/downloads/)
2. In the command line, run `pip3 install requests requests-futures pymisp`. This installs the dependencies necessary to run the script.
3. In the command line, run `PYTHONHASHSEED=0 python3 script.py`. This runs the script.

## Instructions on Reading TiIndicators That Have Been Pushed
In the command line, run `python3 script.py -r`.

## Instructions on Seeing All Requests That Resulted in Errors
1. In the command line, run `cd logs` to go to the logs folder.
2. * To print all the requests that resulted in errors to the console, simply run `cat *_error_*` in the command line.
   * To aggregate all the requests that resulted in errors to a file, run `cat *_error_* > <filename>.txt` in the command line.

## Script Output
As the script runs, it prints out the request body sent to the Graph API and the response from the Graph API.

Every request is logged as a json file under the directory "logs". The name of the json file is the datetime of when the request is completed.

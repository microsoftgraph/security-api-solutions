# MISP to Microsoft Graph Security Script
The <b> MISP to Microsoft Graph Security Script </b> enables you to connect your custom threat indicators or Indicators of Comprosmise (IoCs) and make these available in the following Microsoft products: **[Azure Sentinel](https://azure.microsoft.com/en-us/services/azure-sentinel/)**, **[Microsoft Defender ATP](https://www.microsoft.com/en-us/microsoft-365/windows/microsoft-defender-atp/)**
<br/>
The script provides clients with MISP instances to migrate threat indicators to the [Microsoft Graph Security API](https://aka.ms/graphsecuritydocs). 

For more information on Microsoft Graph Security API visit [Microsoft Graph Security API](https://aka.ms/graphsecuritydocs). <br/>
For more information on Microsoft Graph visit [Microsoft Graph](https://developer.microsoft.com/en-us/graph). <br/>
For more information on MISP visit https://www.misp-project.org/.

## Prerequisites
Before installing the sample:
* Install Python 3.x version from https://www.python.org/.
* To register your application for access to Microsoft Graph, you'll need either a [Microsoft account](https://www.outlook.com/) or an [Office 365 for business account](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account). If you don't have one of these, you can create a Microsoft account for free at [outlook.com](https://www.outlook.com/). 
* Ensure MISP Server is installed and can be connected to - refer to MISP install details at https://www.misp-project.org/download/  

**For more info on how to register app, see "App Registration" section.**

## Getting Started
After the prerequisites are installed or met, perform the following steps to use these scripts:

1. Download or clone this repository.
1. Go to directory `security-api-solutions/Samples/MISP`
1. Install dependencies.  In the command line, run `pip3 install requests requests-futures pymisp` 
1. To run script, go to the root directory of misp-graph-script and enter `PYTHONHASHSEED=0 python3 script.py` in the command line. 

## App Registration
To configure the sample, you'll need to register a new application in the Microsoft [Application Registration Portal](https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/RegisteredApps).
Follow these steps to register a new application:
1. Sign in to the [Application Registration Portal](https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/RegisteredApps) using either your personal or work or school account.

1. Choose **New registration**.

1. Enter an application name, and choose **Register**.

1. Next you'll see the overview page for your app. Copy and save the **Application Id** field. You will need it later to complete the configuration process.

1. Under **Certificates & secrets**, choose **New client secret** and add a quick description. A new secret will be displayed in the **Value** column. Copy this password. You will need it later to complete the configuration process and it will not be shown again.

1. Under **API permissions**, choose **Add a permission > Microsoft Graph**.

1. Under **Application Permissions**, add the permissions/scopes required for the sample. This sample requires **ThreatIndicators.ReadWrite.OwnedBy**.
    >Note: See the [Microsoft Graph permissions reference](https://developer.microsoft.com/en-us/graph/docs/concepts/permissions_reference) for more information about Graph's permission model.
    
1. Modify the RequestManager.py file to comment out line 140-143. (This allows the script to run without failing due to line 123 being divided by `avg_speed` incase it starts as `0`.

1. Modify the script.py to add in `config.misp_verifycert` at line 13. Ensure it looks like below.
```
 misp = PyMISP(config.misp_domain, config.misp_key, config.misp_verifycert)
```

10. Modify config.py file to add in `misp_verifycert = False` anywhere in the file.

As the final step in configuring the script, modify the config.py file in the root folder of your cloned repo.

Update tenant, client_id, and client_secret in config.py
```
graph_auth = {
    'tenant': '<tenant id>',
    'client_id': '<client id>',
    'client_secret': '<client secret>',
}
```

Once changes are complete, save the config file. After you've completed these steps and have received [admin consent](https://github.com/microsoftgraph/python-security-rest-sample#Get-Admin-consent-to-view-Security-data) for your app, you'll be able to run the script.py sample as covered below.

## Configurations
#### Target Product
Possible values for **targetProduct** are: `Azure Sentinel`, `Microsoft Defender ATP`.

#### Misp Event Filter
Filters can be set in the config.py file under the "misp_event_filters" property

Below is a list of parameters that can be passed to the filter (source: https://buildmedia.readthedocs.org/media/pdf/pymisp/latest/pymisp.pdf):
* limit (Optional[int]) – Limit the number of results returned, depending on the
scope (for example, 10 events).
* page (Optional[int]) – If a limit is set, sets the page to be returned. page 3, limit 100
will return records 201->300).
* value (Optional[~SearchParameterTypes]) – Search for the given value in the attributes’ value field.
* type_attribute (Optional[~SearchParameterTypes]) – The attribute type, any
valid MISP attribute type is accepted.
* category (Optional[~SearchParameterTypes]) – The attribute category, any valid
MISP attribute category is accepted.
* org (Optional[~SearchParameterTypes]) – Search by the creator organisation by supplying the organisation identifier.
* tags (Optional[~SearchParameterTypes]) – Tags to search or to exclude. You can
pass a list, or the output of build_complex_query
* quick_filter (Optional[str]) – The string passed to this field will ignore all of
the other arguments. MISP will return an xml / json (depending on the header sent) of all
events that have a sub-string match on value in the event info, event orgc, or any of the
attribute value1 / value2 fields, or in the attribute comment.
* date_from (Union[date, int, str, float, None]) – Events with the date set to a
date after the one specified. This filter will use the date of the event (Format: '2019-01-01').
* date_to (Union[date, int, str, float, None]) – Events with the date set to a date
before the one specified. This filter will use the date of the event (Format: '2019-01-01').
* eventid (Optional[~SearchType]) – The events that should be included / excluded
from the search
* with_attachments (Optional[bool]) – If set, encodes the attachments / zipped
malware samples as base64 in the data field within each attribute
* metadata (Optional[bool]) – Only the metadata (event, tags, relations) is returned,
attributes and proposals are omitted.
* uuid (Optional[str]) – Restrict the results by uuid.
* publish_timestamp (Union[date, int, str, float, None,
Tuple[Union[date, int, str, float, None], Union[date, int, str, float,
None]]]) – Restrict the results by the last publish timestamp (newer than). (Note: Uses UNIX timestamp.  Format: '1551811160').
* timestamp (Union[date, int, str, float, None, Tuple[Union[date, int,
str, float, None], Union[date, int, str, float, None]]]) – Restrict the results
by the timestamp (last edit). Any event with a timestamp newer than the given timestamp
will be returned. In case you are dealing with /attributes as scope, the attribute’s timestamp
will be used for the lookup.
* published (Optional[bool]) – Set whether published or unpublished events should
be returned. Do not set the parameter if you want both.
* enforce_warninglist (Optional[bool]) – Remove any attributes from the result
that would cause a hit on a warninglist entry.
* to_ids (Union[~ToIDSType, List[~ToIDSType], None]) – By default all attributes
are returned that match the other filter parameters, irregardless of their to_ids setting. To
restrict the returned data set to to_ids only attributes set this parameter to 1. 0 for the ones
with to_ids set to False.
* deleted (Optional[str]) – If this parameter is set to 1, it will return soft-deleted
attributes along with active ones. By using “only” as a parameter it will limit the returned
data set to soft-deleted data only.
* include_event_uuid (Optional[bool]) – Instead of just including the event ID,
also include the event UUID in each of the attributes.
* include_event_tags (Optional[bool]) – Include the event level tags in each of
the attributes.
* event_timestamp (Union[date, int, str, float, None]) – Only return attributes from events that have received a modification after the given timestamp (Note: Uses UNIX timestamp.  Format: '1551811160').
* sg_reference_only (Optional[bool]) – If this flag is set, sharing group objects
will not be included, instead only the sharing group ID is set.
* eventinfo (Optional[str]) – Filter on the event’s info field.
* searchall (Optional[bool]) – Search for a full or a substring (delimited by % for
substrings) in the event info, event tags, attribute tags, attribute values or attribute comment
fields.
* requested_attributes (Optional[str]) – [CSV only] Select the fields that you
wish to include in the CSV export. By setting event level fields additionally, includeContext is not required to get event metadata.
* include_context (Optional[bool]) – [Attribute only] Include the event data with
each attribute. [CSV output] Add event level metadata in every line of the CSV.
* headerless (Optional[bool]) – [CSV Only] The CSV created when this setting is
set to true will not contain the header row.
* include_sightings (Optional[bool]) – [JSON Only - Attribute] Include the
sightings of the matching attributes.
* include_decay_score (Optional[bool]) – Include the decay score at attribute
level.
* include_correlations (Optional[bool]) – [JSON Only - attribute] Include the
correlations of the matching attributes.

A list or a specific value can be passed to the above parameters. If a list is passed to the parameter, the filtered events are the result of the union of provided list.

This field needs to be a list that contains multiple filters. The filtered events are the result of the intersection of provided filters.

#### First Example of How This Field can be Configured
```
misp_event_filters = {
    "type_attribute": ['mutex', 'filename|md5']
}
```
An event meets this filtering criteria if the event has an attribute with attribute type of 'mutex' OR the event has an attribute with attribute type of 'filename|md5'.

#### Second Example of How This Field can be Configured
```
misp_event_filters = {
    "value": 'http://www.test.com'
}
```
An event meets this filtering criteria if the event has an attribute with attribute value of 'http://www.test.com'.

#### Third Example of How This Field can be Configured
```
misp_event_filters = {}
```
This gets all events.

#### Action
Possible **action** values are: `alert`, `allow`, `block`.

#### Passive Only
`passiveOnly = False`

#### Days to Expire
This property is used to specify the amount of days the records will expire in Microsoft Graph Security API. The default value for days to expire is 30.  

`days_to_expire = 5`

#### Misp Key
The Misp Key is required to fetch data from your Misp instance. 
It can be found in the event actions menu under automation on the website of the Misp instance.

`misp_key = '<misp key>'`

#### Misp Domain
Misp Domain is the base URL of your MISP instance.

#### Misp Verify Cert
This gives you the option to choose if python should validate the certificate of the misp instance. This allows ease within testing environments.
It is recommended to use a valid SSL cert in production and change this value to True.

`misp_verifycert = False` 

## Instructions on Reading TiIndicators That Have Been Pushed
In the command line, run `python3 script.py -r`

## Instructions on Seeing All Requests That Resulted in Errors
1. In the command line, run `cd logs` to go to the logs folder.
2. * To print all the requests that resulted in errors to the console, simply run `cat *_error_*` in the command line.
   * To aggregate all the requests that resulted in errors to a file, run `cat *_error_* > <filename>.txt` in the command line.

## Script Output
As the script runs, it prints out the request body sent to the Microsoft Graph Security API and the response from the Microsoft Graph Security API.

Every request is logged as a json file under the directory "logs". The name of the json file is the datetime of when the request is completed.

## Schedule with CRONTAB
Below is a CRONTAB entry example of running the script every Sunday at 2am

0 2 * * Sun /home/mark/misp-graph-script/python3 script.sh

## Contributing
If you'd like to contribute to this sample, see [CONTRIBUTING.MD](https://github.com/microsoftgraph/security-api-solutions/blob/master/CONTRIBUTING.md).

This project has adopted the Microsoft Open Source Code of Conduct. For more information, see the Code of Conduct FAQ or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

## Questions and comments
We'd love to get your feedback about the MISP to Microsoft Graph Security script. You can send your questions and suggestions to us in the Issues section of this repository.

Your feedback is important to us. Connect with us on [Microsoft tech community](https://techcommunity.microsoft.com/t5/Using-Microsoft-Graph-Security/bd-p/SecurityGraphAPI) or [Stack Overflow](https://stackoverflow.com/questions/tagged/microsoft-graph-security). On Stack Overflow tag your questions with [microsoft-graph-security].

## Additional resources
* [Microsoft Graph Security Documentation](https://developer.microsoft.com/en-us/graph/docs/concepts/security-concept-overview)
* [Microsoft Graph Explorer](https://developer.microsoft.com/en-us/graph/graph-explorer)
* [Microsoft code samples](https://developer.microsoft.com/en-us/graph/code-samples-and-sdks)
* [MISP to Microsoft Graph Security connector](https://www.circl.lu/doc/misp/connectors/#misp-to-microsoft-graph-security-script)

### Copyright
Copyright (c) 2018 Microsoft. All rights reserved.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information, see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

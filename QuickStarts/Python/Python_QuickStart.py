# -------------------------------------------------------------------------
# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License. 
# --------------------------------------------------------------------------

## Visit https://aka.ms/graphsecuritydocs for the Microsoft Graph Security API documention.

import json
import urllib.request
import urllib.parse
import requests 

## Register an Azure Active Directory application with the 'SecurityEvents.ReadWrite.All' Microsoft Graph Permission.
## Get your Azure AD tenant administrator to grant administration consent to your application. This is a one-time activity unless permissions change for the application. 
appId = 'Enter_Your_App_ID_Here'
appSecret = 'Enter_Your_App_Secret_Here' 
tenantId = 'Enter_Your_Tenant_ID_Here'

# Azure Active Directory token endpoint.
url = "https://login.microsoftonline.com/%s/oauth2/v2.0/token" % (tenantId)
body = {
    'client_id' : appId,
    'client_secret' : appSecret,
    'grant_type' : 'client_credentials',
    'scope': 'https://graph.microsoft.com/.default'
}

## authenticate and obtain AAD Token for future calls
data = urllib.parse.urlencode(body).encode("utf-8") # encodes the data into a 'x-www-form-urlencoded' type
req = urllib.request.Request(url, data)

response = urllib.request.urlopen(req)

jsonResponse = json.loads(response.read().decode())
# Grab the token from the response then store it in the headers dict.
aadToken = jsonResponse["access_token"]
headers = { 
    'Content-Type' : 'application/json',
    'Accept' : 'application/json',
    'Authorization' : "Bearer " + aadToken
}
api_root = "https://graph.microsoft.com/beta/" 
if len(aadToken) > 0:
    print("Access token acquired.")


def make_request(url):
    """
    Makes a GET request. This can be applied to alerts, TI indicators, secure scores.

    :param url: Url of the request.
    :returns: json response.
    :raises HTTPError: raises an exception
    """
    url_sanitized = urllib.parse.quote( url , safe="%/:=&?~#+!$,;'@()*[]") # Url encode spaces 
    req = urllib.request.Request(url_sanitized, headers=headers)
    print()
    print("########################################################################################")
    print("Calling the Microsoft Graph Security API...")
    print()
    print('GET "%s"' % url_sanitized)
    print()
    print("Headers :")
    print(json.dumps(headers, indent=4))
    print("########################################################################################")

    try:
        response = urllib.request.urlopen(req)
    except urllib.error.HTTPError as e:
        raise e

    jsonResponse = json.loads(response.read().decode())
    print()
    print("Response :")
    print(json.dumps(jsonResponse, indent=4))
    return jsonResponse

print("Sample Microsoft Graph Security API code loaded")

##### Uncomment the code below to get the most recent high severity alert from each provider.
#alert_url = "https://graph.microsoft.com/beta/security/alerts?$filter=severity eq 'high'&$top=1"
#ti_url = "https://graph.microsoft.com/beta/security/tiIndicators"
#securescore_url = "https://graph.microsoft.com/beta/security/secureScores"
#make_request(alert_url)
#make_request(ti_url)
#make_request(securescore_url)

##### Uncomment the code below to get an alert or TI indicator by id
# alert_url = "https://graph.microsoft.com/beta/security/alerts/{id}"
# ti_url = "https://graph.microsoft.com/beta/security/tiIndicators/{id}"
# make_request(alert_url)
# make_request(ti_url)


def update_alert(alertid=None, alertbody=None):
    """
    Makes a PATCH request to update an alert.

    :param alertid: Id of the alert to be updated.
    :param alertbody: Body of the PATCH request.
    :returns: json response.
    :raises ValueError: raises an exception
    """
    alert_url = "https://graph.microsoft.com/beta/security/alerts/%s" % alertid
    
    if alertbody == None:
        raise ValueError ('Request body cannot be empty')

    # Add the 'Prefer' header in order to get the updated alert back from the API after the PATCH. This is optional.
    patch_headers = headers
    patch_headers['Prefer'] = "return=representation"
    
    # vendorInformation is required by the API when PATCHing alerts, 
    # so we first make a call to GET the alert that we would like to PATCH.
    oldalert = requests.get(alert_url, headers=headers).json()
    # Then add 'vendorInformation' to the PATCH body.
    alertbody["vendorInformation"] = oldalert["vendorInformation"]

    print()
    print("########################################################################################")
    print("Calling the Microsoft Graph Security API...")
    print()
    print('PATCH "%s"' % alert_url)
    print()
    print("Headers :")
    print(json.dumps(headers, indent=4)) 
    print()   
    print("Body :")
    print(json.dumps(alertbody, indent=4))    
    print("########################################################################################")
    print()   
    
    # Make the PATCH request.
    response = requests.patch(alert_url, json.dumps(alertbody), headers=patch_headers)
    if response.status_code == 200:
        # if the response returned a 200, convert it to json
        response = response.json()
        print("Response :")
        print(json.dumps(response, indent=4))
    else:
        print("Response : Status Code", response.status_code)
        response = {}

    return response

##### Uncomment the code below to update an alert
# data = {"comments": ["Testing"]}
# update_alert(alertid="<an alertId>", alertbody=data)

def create_ti(tibody=None):
    """
    Makes a POST request to create a TI indicator.

    :param tibody: Body of the POST request.
    :returns: json response.
    :raises ValueError: raises an exception
    """
    ti_url = "https://graph.microsoft.com/beta/security/tiIndicators"
    
    #The request body must contain at least one email, file, or network observable.
    if tibody == None:
        raise ValueError ('Request body cannot be empty')

    post_headers = headers
    print()
    print("########################################################################################")
    print("Calling the Microsoft Graph Security API...")
    print()

    print('CREATE "%s"' % ti_url)
    print()
    print("Headers :")
    print(json.dumps(headers, indent=4)) 
    print()   
    print("Body :")
    print(json.dumps(tibody, indent=4))    
    print("########################################################################################")
    print()

    # Make the POST request.
    response = requests.post(ti_url, json.dumps(tibody), headers=post_headers)
    if response.status_code == 201:
        # if the response returned a 201, convert it to json
        response = response.json()
        print("Response :")
        print(json.dumps(response, indent=4))
    else:
        print("Response : Status Code", response.status_code)
        response = {}

    return response

##### Uncomment the code below to post a TI
# data = {
#     "action": "alert",
#     "activityGroupNames": [],
#     "confidence": 0,
#     "description": "This is a canary indicator for demo purpose. Take no action on any observables set in this indicator.",
#     "expirationDateTime": "2019-12-31T21:43:37.5031462+00:00",
#     "externalId": "Test--123456",
#     "fileHashType": "sha256",
#     "fileHashValue": "aa64428620b57bf51524d1756b2ed746e5a3f31b67cf7fe5b5d8a9daf07ca313",
#     "killChain": [],
#     "malwareFamilyNames": [],
#     "severity": 0,
#     "tags": [],
#     "targetProduct": "Microsoft Defender ATP",
#     "threatType": "WatchList",
#     "tlpLevel": "green"
# }
# create_ti(tibody=data)


def update_ti(tiid=None, tibody=None):
    """
    Makes a PATCH request to update a TI indicator.

    :param tiid: Id of the TI to be updated.
    :param tibody: Body of the PATCH request.
    :returns: json response.
    :raises ValueError: raises an exception
    """
    ti_url = "https://graph.microsoft.com/beta/security/tiIndicators/%s" % tiid
    
    if tibody == None:
        raise ValueError ('Request body cannot be empty')
    # Add the 'Prefer' header in order to get the updated TI Indicator back from the API after the PATCH. This is optional.
    patch_headers = headers
    patch_headers['Prefer'] = "return=representation"
    
    # targetProduct and expirationDateTime are required by the API when PATCHing TIs, 
    # so we first make a call to GET the TI that we would like to PATCH.
    oldti = requests.get(ti_url, headers=headers).json()
    # Then add 'targetProduct' to the PATCH body.
    tibody["targetProduct"] = oldti["targetProduct"]
    tibody["expirationDateTime"] = oldti["expirationDateTime"]

    print()
    print("########################################################################################")
    print("Calling the Microsoft Graph Security API...")
    print()
    print('PATCH "%s"' % ti_url)
    print()
    print("Headers :")
    print(json.dumps(headers, indent=4)) 
    print()   
    print("Body :")
    print(json.dumps(tibody, indent=4))    
    print("########################################################################################")
    print()   
    
    # Make the PATCH request.
    response = requests.patch(ti_url, json.dumps(tibody), headers=patch_headers)
    if response.status_code == 200:
        # if the response returned a 200, convert it to json
        response = response.json()
        print("Response :")
        print(json.dumps(response, indent=4))
    else:
        print("Response : Status Code", response.status_code)
        response = {}

    return response

##### Uncomment the code below to update a TI
# data = {"additionalInformation": ["Some extra data from the indicator not covered by the other tiIndicator properties"]}
# update_ti(tiid="<test tiId>", tibody=data)

def delete_ti(tiid = None):
    """
    Makes a DELETE request to remove a TI indicator.

    :param tiid: Id of the TI to be deleted.
    :returns: json response.
    :raises ValueError: raises an exception
    """
    ti_url = "https://graph.microsoft.com/beta/security/tiIndicators/%s" % tiid
    delete_headers = headers

    print()
    print("########################################################################################")
    print("Calling the Microsoft Graph Security API...")
    print()
    print('DELETE "%s"' % ti_url)
    print()
    print("Headers :")
    print(json.dumps(headers, indent=4)) 
    print() 
    

    # Make the DELETE request.
    response = requests.delete(ti_url, headers=delete_headers)
    if response.status_code == 204:
        # if the response returned a 204, print success response
        print("Response : successfully deleted TI", response.status_code)
    else:
        print("Response : Status Code", response.status_code)
        response = {}

    return response

##### Uncomment the code below to delete a TI
# delete_ti(tiid="<test tiId>", tibody=data)


def create_bulk_tis(tibody=None):
    """
    Makes a POST request to create multiple TI indicators.

    :param tibody: Body of the POST request.
    :returns: json response.
    :raises ValueError: raises an exception
    """
    ti_url = "https://graph.microsoft.com/beta/security/tiIndicators/submitTiIndicators"
    
    #The request body must contain at least one email, file, or network observable.
    if tibody == None:
        raise ValueError ('Request body cannot be empty')

    post_headers = headers
    print()
    print("########################################################################################")
    print("Calling the Microsoft Graph Security API...")
    print()
    print('POST "%s"' % ti_url)
    print()
    print("Headers :")
    print(json.dumps(headers, indent=4)) 
    print()   
    print("Body :")
    print(json.dumps(tibody, indent=4))    
    print("########################################################################################")
    print()

    # Make the POST request.
    response = requests.post(ti_url, json.dumps(tibody), headers=post_headers)
    if response.status_code == 200:
        # if the response returned a 200, convert it to json
        response = response.json()
        print("Response :")
        print(json.dumps(response, indent=4))
    else:
        print("Response Status Code: ", response.status_code)
        print("Response Message: ", response.content)
        response = {}

    return response

##### Uncomment the code below to post bulk TIs
# data = {
#     "value":
#     [
#         {
#             "activityGroupNames": [],
#             "confidence": 0,
#             "description": "This is a canary indicator for demo purpose. Take no action on any observables set in this indicator.",
#             "expirationDateTime": "2019-12-31T21:44:03.1668987+00:00",
#             "externalId": "Test--1",
#             "fileHashType": "sha256",
#             "fileHashValue": "b555c4205b1b01304217e72118d6ca1b14b7013644a078273cea27bbdc1cf9d6",
#             "killChain": [],
#             "malwareFamilyNames": [],
#             "severity": 0,
#             "tags": [],
#             "targetProduct": "Azure Sentinel",
#             "threatType": "WatchList",
#             "tlpLevel": "green",
#         },
#         {
#             "activityGroupNames": [],
#             "confidence": 0,
#             "description": "This is a canary indicator for demo purpose. Take no action on any observables set in this indicator.",
#             "expirationDateTime": "2019-12-31T21:44:03.1748779+00:00",
#             "externalId": "Test--2",
#             "fileHashType": "sha256",
#             "fileHashValue": "1796c233950990b28d6a22456c9d2b58ced1bdfcdf5f16f7e39d6b9bdca4213b",
#             "killChain": [],
#             "malwareFamilyNames": [],
#             "severity": 0,
#             "tags": [],
#             "targetProduct": "Azure Sentinel",
#             "threatType": "WatchList",
#             "tlpLevel": "green",
#         }
#     ]
# }
#create_bulk_tis(tibody=data)

def update_bulk_tis(tibody=None):
    """
    Makes a POST request to update multiple TI indicators. Currently support 'Azure Sentinel' targetProduct only.

    :param tibody: Body of the POST request.
    :returns: json response.
    :raises ValueError: raises an exception
    """
    ti_url = "https://graph.microsoft.com/beta/security/tiIndicators/updateTiIndicators"
    updatedbody = {'value': []}
    if tibody == None:
        raise ValueError ('Request body cannot be empty')

    # targetProduct and expirationDateTime are required by the API when PATCHing TIs,
	# so we first make a call to get the TI list we'd like to PATCH'
    for ti in tibody:
        old_ti = requests.get(ti_url, headers=headers).json()
		# then add 'targetProduct' and 'expirationDateTime' to the PATCH body.
        ti["targetProduct"] = old_ti["targetProduct"]
        ti["expirationDateTime"] = old_ti["expirationDateTime"]
	# update request body object
    updatedbody['value'] = tibody

    print()
    print("########################################################################################")
    print("Calling the Microsoft Graph Security API...")
    print()
    print('UPDATE "%s"' % ti_url)
    print()
    print("Headers :")
    print(json.dumps(headers, indent=4)) 
    print()   
    print("Body :")
    print(json.dumps(updatedbody, indent=4))    
    print("########################################################################################")
    print()

    # Make the POST request.
    response = requests.post(ti_url, json.dumps(updatedbody), headers=headers)
    if response.status_code == 200:
        # if the response returned a 200, convert it to json
        response = response.json()
        print("Response :")
        print(json.dumps(response, indent=4))
    else:
        print("Response Status Code: ", response.status_code)
        print("Response Message: ", response.content)
        response = {}

    return response

##### Uncomment the code below to update bulk TIs
# data = [
#	{
#     "id": "<ti_id1>",
#     "additionalInformation": "mytest"
#	},
#	{
#     "id": "<ti_id2>",
#     "additionalInformation": "test again"
#	}
#]
# update_bulk_tis(data)


def delete_bulk_tis(tibody=None):
    """
    Makes a POST request to delete multiple TI indicators. Currently support 'Azure Sentinel' targetProduct only.

    :param tibody: Body of the POST request.
    :returns: json response.
    :raises ValueError: raises an exception
    """
    ti_url = "https://graph.microsoft.com/beta/security/tiIndicators/deleteTiIndicators"

    if tibody == None:
        raise ValueError ('Request body cannot be empty')

    post_headers = headers
    print()
    print("########################################################################################")
    print("Calling the Microsoft Graph Security API...")
    print()
    print('DELETE "%s"' % ti_url)
    print()
    print("Headers :")
    print(json.dumps(headers, indent=4)) 
    print()   
    print("Body :")
    print(json.dumps(tibody, indent=4))    
    print("########################################################################################")
    print()

    # Make the POST request.
    response = requests.post(ti_url, json.dumps(tibody), headers=post_headers)
    if response.status_code == 200:
        # if the response returned a 200, convert it to json
        response = response.json()
        print("Response :")
        print(json.dumps(response, indent=4))
    else:
        print("Response Status Code: ", response.status_code)
        print("Response Message: ", response.content)
        response = {}

    return response

##### Uncomment the code below to delete bulk TIs
# data = {
#   "value": [
#     "id-value1",
#     "id-value2"
#   ]
# }
# delete_bulk_tis(data)


def delete_bulk_by_externalIds(tibody=None):
    """
    Makes a POST request to delete multiple TI indicators by external IDs. Currently support 'Azure Sentinel' targetProduct only.

    :param tibody: Body of the POST request.
    :returns: json response.
    :raises ValueError: raises an exception
    """
    ti_url = "https://graph.microsoft.com/beta/security/tiIndicators/deleteTiIndicatorsByExternalId"

    if tibody == None:
        raise ValueError ('Request body cannot be empty')

    post_headers = headers
    print()
    print("########################################################################################")
    print("Calling the Microsoft Graph Security API...")
    print()
    print('DELETE "%s"' % ti_url)
    print()
    print("Headers :")
    print(json.dumps(headers, indent=4)) 
    print()   
    print("Body :")
    print(json.dumps(tibody, indent=4))    
    print("########################################################################################")
    print()

    # Make the POST request.
    response = requests.post(ti_url, json.dumps(tibody), headers=post_headers)
    if response.status_code == 200:
        # if the response returned a 200, convert it to json
        response = response.json()
        print("Response :")
        print(json.dumps(response, indent=4))
    else:
        print("Response Status Code: ", response.status_code)
        print("Response Message: ", response.content)
        response = {}

    return response

##### Uncomment the code below to delete bulk TIs
# data = {
#   "value": [
#     "id-value1",
#     "id-value2"
#   ]
# }
# delete_bulk_by_externalIds(data)

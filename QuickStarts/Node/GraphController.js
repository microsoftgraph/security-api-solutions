'use strict'
const request = require('request-promise');

/*
 *  Example controller to handle Microsoft Graph Security API requests.
 */
function GraphSecurityAPI(config) {
  this.appId = config.appId;
  this.appSecret = config.appSecret;
  this.tenantId = config.tenantId;
  this.token = null;
}

GraphSecurityAPI.prototype.storeToken = function(token) {
  this.token = token;
}

/*
 *  Creates a POST request to receive OAuth authorization token for your tenant ID.
 */
GraphSecurityAPI.prototype.getAuthenticationToken = function () {
  const options = {
    uri: `https://login.microsoftonline.com/${this.tenantId}/oauth2/v2.0/token`,
    method: 'POST',
    form: {
      client_id: this.appId,
      client_secret: this.appSecret,
      grant_type: 'client_credentials',
      scope: 'https://graph.microsoft.com/.default'
    },
    json: true
  };
  return request(options)
}

/*
 *  Creates a GET request to retrieve all security alerts.
 */
GraphSecurityAPI.prototype.getAlerts = function() {
  const options = {
    uri: 'https://graph.microsoft.com/v1.0/security/alerts',
    method: 'GET',
    headers: { 'Authorization': 'Bearer ' + this.token.access_token },
    json: true
  };
  return request(options);
}

/*
 * Creates a GET request to retrieve a single security alert.
 */
GraphSecurityAPI.prototype.getOneAlert = function(alertId) {
  const options = {
    uri: 'https://graph.microsoft.com/v1.0/security/alerts/' + alertId,
    method: 'GET',
    headers: { 'Authorization': 'Bearer ' + this.token.access_token },
    json: true
  };
  return request(options);
}

/*
 *  Creates a PATCH request to update a security alert.
 */
GraphSecurityAPI.prototype.updateAlert = function(alertId) {
  return this.getOneAlert(alertId)
    .then(alertInfo => {
      const options = {
        uri: 'https://graph.microsoft.com/v1.0/security/alerts/' + alertId,
        method: 'PATCH',
        form: {
          assignedTo: 'The Knights who say Ni',
          comments: [
            'These are sample comments being updated to the entry',
            'Rubber Baby Buggy Bumpers',
            'The only requirement for PATCH is the vendor information'
          ],
          vendorInformation: alertInfo.vendorInformation
        },
        headers: {
          'Authorization': 'Bearer ' + this.token.access_token,
          'Prefer': 'return=representation'
        },
        json: true
      };
      options.form = JSON.stringify(options.form);
      return request(options);
    })
}

/*
 *  Creates a GET request to retrieve all secure scores.
 */
GraphSecurityAPI.prototype.getSecureScores = function() {
  const options = {
    uri: 'https://graph.microsoft.com/beta/security/secureScores',
    method: 'GET',
    headers: { 'Authorization': 'Bearer ' + this.token.access_token },
    json: true
  };
  return request(options);
}

/*
 *  Creates a GET request to retrieve all TI indicators.
 */
GraphSecurityAPI.prototype.getTIs = function() {
  const options = {
    uri: 'https://graph.microsoft.com/beta/security/tiIndicators',
    method: 'GET',
    headers: { 'Authorization': 'Bearer ' + this.token.access_token },
    json: true
  };
  return request(options);
}

/*
 * Creates a GET request to retrieve a single TI indicator.
 */
GraphSecurityAPI.prototype.getOneTI = function(tiId) {
  const options = {
    uri: 'https://graph.microsoft.com/beta/security/tiIndicators/' + tiId,
    method: 'GET',
    headers: { 'Authorization': 'Bearer ' + this.token.access_token },
    json: true
  };
  return request(options);
}

/*
 * Creates a POST request to submit a single TI indicator.
 */
GraphSecurityAPI.prototype.createTI = function() {
  const options = {
    uri: 'https://graph.microsoft.com/beta/security/tiIndicators',
    method: 'POST',
    form:  {
      "action": "alert",
      "activityGroupNames": [],
      "confidence": 0,
      "description": "This is a canary indicator for demo purpose. Take no action on any observables set in this indicator.",
      "expirationDateTime": "2019-11-30T21:43:37.5031462+00:00",
      "externalId": "Test--8586509942679764298MS501",
      "fileHashType": "sha256",
      "fileHashValue": "aa64428647b57bf51524d1756b2ed746e5a3f31b67cf7fe5b5d8a9daf07ca313",
      "killChain": [],
      "malwareFamilyNames": [],
      "severity": 0,
      "tags": [],
      "targetProduct": "Azure Sentinel",
      "threatType": "WatchList",
      "tlpLevel": "green"
      },
    headers: { 'Authorization': 'Bearer ' + this.token.access_token },
    json: true
  };
  options.form = JSON.stringify(options.form);
  return request(options);
}

/*
 *  Creates a PATCH request to update a TI indicator.
 */
GraphSecurityAPI.prototype.updateTI = function(tiId) {
  return this.getOneTI(tiId)
    .then(tiInfo => {
      const options = {
        uri: 'https://graph.microsoft.com/beta/security/tiIndicators/' + tiId,
        method: 'PATCH',
        form: {
          additionalInformation: 'targetProduct & expirationDateTime are required for PATCH TIs',
          targetProduct: tiInfo.targetProduct,
          expirationDateTime: tiInfo.expirationDateTime
        },
        headers: {
          'Authorization': 'Bearer ' + this.token.access_token,
          'Prefer': 'return=representation'
        },
        json: true
      };
      options.form = JSON.stringify(options.form);
      return request(options);
    })
}

/*
 * Creates a DELETE request to delete a single TI indicator.
 */
GraphSecurityAPI.prototype.deleteOneTI = function(tiId) {
  const options = {
    uri: 'https://graph.microsoft.com/beta/security/tiIndicators/' + tiId,
    method: 'DELETE',
    headers: { 'Authorization': 'Bearer ' + this.token.access_token },
    json: true
  };
  return request(options);
}

/*
 * Creates a POST request to submit multiple TI indicators.
 */
GraphSecurityAPI.prototype.createTIs = function() {
  const options = {
    uri: 'https://graph.microsoft.com/beta/security/tiIndicators/submitTiIndicators',
    method: 'POST',
    form: {
      "value": [
        {
         "activityGroupNames": [],
         "confidence": 0,
         "description": "This is a canary indicator for demo purpose. Take no action on any observables set in this indicator.",
         "expirationDateTime": "2019-11-30T21:44:03.1668987+00:00",
         "externalId": "Test--8586509942423126760MS164-0",
         "fileHashType": "sha256",
         "fileHashValue": "b555c45c5b1b01304217e72118d6ca1b14b7013644a078273cea27bbdc1cf9d6",
         "killChain": [],
         "malwareFamilyNames": [],
         "severity": 0,
         "tags": [],
         "targetProduct": "Azure Sentinel",
         "threatType": "WatchList",
         "tlpLevel": "green",
        },
        {
         "activityGroupNames": [],
         "confidence": 0,
         "description": "This is a canary indicator for demo purpose. Take no action on any observables set in this indicator.",
         "expirationDateTime": "2019-03-01T21:44:03.1748779+00:00",
         "externalId": "Test--8586509942423126760MS164-1",
         "fileHashType": "sha256",
         "fileHashValue": "1796b433950990b28d6a22456c9d2b58ced1bdfcdf5f16f7e39d6b9bdca4213b",
         "killChain": [],
         "malwareFamilyNames": [],
         "severity": 0,
         "tags": [],
         "targetProduct": "Azure Sentinel",
         "threatType": "WatchList",
         "tlpLevel": "green",
       }
     ]
   },
    headers: { 'Authorization': 'Bearer ' + this.token.access_token },
    json: true
  };
  options.form = JSON.stringify(options.form);
  return request(options);
}

/*
 * Creates a POST request to update multiple TI indicators.
 */
GraphSecurityAPI.prototype.updateTIs = function(data) {
  let formData = {'value': []};

  for(i = 0; i < data.length; i++){
    ti = this.getOneTI(data[i]['id']);
    data[i]['targetProduct'] = ti['targetProduct'];
    data[i]['expirationDateTime'] = ti['expirationDateTime'];
  };
  formData['value'] = data;

  return formData
    .then(formData => {
      const options = {
        uri: 'https://graph.microsoft.com/beta/security/tiIndicators/updateTiIndicators',
        method: 'POST',
        form: formData,
        headers: { 'Authorization': 'Bearer ' + this.token.access_token },
        json: true
      };
      options.form = JSON.stringify(options.form);
      return request(options);
    })
}

//example UpdateTIs data
/*
data = [
  {
    "id": "id-value1",
    "additionalInformation": "mytest"
  },
  {
    "id": "id-value2",
    "additionalInformation": "test again"
  }
];
*/

/*
 * Creates a POST request to delete multiple TI indicators.
 */
GraphSecurityAPI.prototype.deleteTIs = function(data) {
  const options = {
    uri: 'https://graph.microsoft.com/beta/security/tiIndicators/deleteTiIndicators',
    method: 'POST',
    form: data,
    headers: { 'Authorization': 'Bearer ' + this.token.access_token },
    json: true
  };
  options.form = JSON.stringify(options.form);
  return request(options);
}
 //example DeleteTIs data
/*
data = {
  "value": [
    "id-value1",
    "id-value2"
  ]
}
*/

/*
 * Creates a POST request to delete multiple TI indicators by External Ids.
 */
GraphSecurityAPI.prototype.deleteTIsByExternalId = function(data) {
  const options = {
    uri: 'https://graph.microsoft.com/beta/security/tiIndicators/deleteTiIndicatorsByExternalId',
    method: 'POST',
    form: data,
    headers: { 'Authorization': 'Bearer ' + this.token.access_token },
    json: true
  };
  options.form = JSON.stringify(options.form);
  return request(options);
}
 //example DeleteTIsByExternalId data
/*
data = {
  "value": [
    "externalId-value1",
    "externalId-value2"
  ]
}
*/

module.exports = GraphSecurityAPI;
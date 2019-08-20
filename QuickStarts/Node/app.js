'use strict'
const request = require('request-promise');

// Application information bellow
const appId = 'abca2d54-2fe9-430a-a2fb-485faf29b335';
const appSecret = '01a4hpk6MCEUBX]mbYmgVS?]oRSh+=D1';
const tenantId = '63d0b44f-8610-4dd2-91fd-541ffc973399';

// Placeholders for sample queries
let token;
let alert;
Run();

function Run() {
  // Create POST request to receive authentication token for Graph API requests
  const options = {
    uri: `https://login.microsoftonline.com/${tenantId}/oauth2/v2.0/token`,
    method: 'POST',
    form: {
      client_id: appId,
      client_secret: appSecret,
      grant_type: 'client_credentials',
      scope: 'https://graph.microsoft.com/.default'
    },
    json: true
  };
  request(options)
    .then(data => {
      console.log('\n---------- Received Auth Token -----------\n');
      console.log(data);
      token = data;
      return getAlerts();
    })
    .then(data => {
      console.log('\n---------- Received All Alerts -----------\n');
      console.log('Received ' + data.value.length + ' alerts...');
      console.log('Printing first alert');
      console.log(data.value[0]);
      alert = data.value[0];
      return getOneAlert(alert.id);
    })
    .then(data => {
      console.log('\n---------- Received One Alert by ID -----------\n');
      console.log(data);
      console.log('\nPrinting comments');
      console.log('Comments: ', data.comments);
      return updateAlert(data);
    })
    .then(data => {
      console.log('\n---------- Updated One Alert by ID -----------\n');
      console.log(data);
      console.log('\nPrinting comments');
      console.log('Comments: ', data.comments);
    })
    .catch(err => console.log(err.message));
}

// Create GET request to retrieve all security alerts
function getAlerts() {
  const options = {
    uri: 'https://graph.microsoft.com/v1.0/security/alerts',
    method: 'GET',
    headers: { 'Authorization': 'Bearer ' + token.access_token },
    json: true
  };
  return request(options)
}

// Create GET request to retrieve a single security alert
function getOneAlert(alertId) {
  const options = {
    uri: 'https://graph.microsoft.com/v1.0/security/alerts/' + alertId,
    method: 'GET',
    headers: { 'Authorization': 'Bearer ' + token.access_token },
    json: true
  };
  return request(options)
}

// Create PATCH request to update a security alert
function updateAlert(alert) {
  const options = {
    uri: 'https://graph.microsoft.com/v1.0/security/alerts/' + alert.id,
    method: 'PATCH',
    form: {
      comments: ['This is an updated comment example'],
      vendorInformation: alert.vendorInformation
    },
    headers: {
      'Authorization': 'Bearer ' + token.access_token,
      'Prefer': 'return=representation'
    },
    json: true
  };
  options.form = JSON.stringify(options.form);
  return request(options);
}
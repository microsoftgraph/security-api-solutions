'use strict'
const GraphSecurityAPI = require('./GraphController');
const config = require('./config');
const print = require('./etc/logHelper');

RunAlert();
RunTI();
RunTIs();
RunSecureScore();

function RunAlert() {
  const GraphSecurity = new GraphSecurityAPI(config);
  GraphSecurity.getAuthenticationToken()
    .then(token => {
      print.logToken(token);
      GraphSecurity.storeToken(token);
      return GraphSecurity.getAlerts();
    })
    .then(alerts => { 
      print.logAlerts(alerts);
      return GraphSecurity.getOneAlert(alerts.value[0].id)
    })
    .then(alertData => {
      print.logAlert(alertData);
      return GraphSecurity.updateAlert(alertData.id);
    })
    .then(updatedAlert => {
      print.logUpdateAlert(updatedAlert);
    })
    .catch(err => console.log(err.message));
}

function RunTI() {
  const GraphSecurity = new GraphSecurityAPI(config);
  GraphSecurity.getAuthenticationToken()
    .then(token => {
      print.logToken(token);
      GraphSecurity.storeToken(token);
      return GraphSecurity.getTIs();
    })
    .then(indicators => { 
      print.logTIs(indicators);
      return GraphSecurity.getOneTI(indicators.value[0].id)
    })
    .then(tiData => {
      print.logTI(tiData);
      return GraphSecurity.updateTI(tiData.id);
    })
    .then(updatedTI => {
      print.logUpdateTI(updatedTI);
      return GraphSecurity.deleteTI(tiData.id);
    })
    .then(updatedTI => {
      print.logDeleteTI(updatedTI);
    })
    .catch(err => console.log(err.message));
}

function RunTIs() {
  const GraphSecurity = new GraphSecurityAPI(config);
  GraphSecurity.getAuthenticationToken()
    .then(token => {
      print.logToken(token);
      GraphSecurity.storeToken(token);
      return GraphSecurity.createTIs(data);
    })
    .then(createdTIs => { 
      print.logCreateTIs(createdTIs);
      return GraphSecurity.updateTIs({ "value": [createdTIs] });
    })
    .then(tiData => {
      print.logUpdateTIs(tiData);
      return GraphSecurity.deleteTIs({ "value": [tiData] });
    })
    .then(updatedTIs => {
      print.logDeleteTIs(updatedTIs);
    })
    .catch(err => console.log(err.message));
}

function RunSecureScore() {
  const GraphSecurity = new GraphSecurityAPI(config);
  GraphSecurity.getAuthenticationToken()
    .then(token => {
      print.logToken(token);
      GraphSecurity.storeToken(token);
      return GraphSecurity.getSecureScores();
    })
    .then(secureScores => { 
      print.logSecurescores(secureScores);
    })
    .catch(err => console.log(err.message));
}
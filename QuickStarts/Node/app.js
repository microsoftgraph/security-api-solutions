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
    .then(createdTI => {
      print.logTI(createdTI);
      return GraphSecurity.updateTI(createdTI.id);
    })
    .then(updatedTI => {
      print.logUpdateTI(updatedTI);
      return GraphSecurity.deleteTI(tiData.id);
    })
    .then(deletedTI => {
      print.logDeleteTI(deletedTI);
    })
    .catch(err => console.log(err.message));
}

function RunTIs() {
  const GraphSecurity = new GraphSecurityAPI(config);
  GraphSecurity.getAuthenticationToken()
    .then(token => {
      print.logToken(token);
      GraphSecurity.storeToken(token);
      return GraphSecurity.createTIs();
    })
    .then(createdTIs => { 
      print.logCreateTIs(createdTIs);
      return GraphSecurity.updateTIs({ "value": [createdTIs] });
    })
    .then(updatedTIs => {
      print.logUpdateTIs(updatedTIs);
      return GraphSecurity.deleteTIs({ "value": [updatedTIs] });
    })
    .then(deletedTIs => {
        print.logDeleteTIs(deletedTIs);
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
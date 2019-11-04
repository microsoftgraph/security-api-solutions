function logToken(token) {
  console.log(token);
  console.log('*'.repeat(80));
  console.log('Getting All Alerts');
  console.log('*'.repeat(80));
}

function logAlerts(alerts) {
  console.log(`Received ${alerts.value.length} alerts`);
  console.log('*'.repeat(80));
  console.log('Getting the First Alert in the Value Array');
  console.log('*'.repeat(80));
}

function logAlert(alert) {
  console.log(alert);
  console.log('*'.repeat(80));
  console.log('Updating the First Alert');
  console.log('*'.repeat(80));
}

function logUpdateAlert(alert) {
  console.log('See Updated Alert Below');
  console.log('*'.repeat(80));
  console.log(alert);
}

function logTIs(tiIndicators){
  console.log(`Received ${tiIndicators.value.length} TIs`);
  console.log('*'.repeat(80));
  console.log('Getting the First TI in the Value Array');
  console.log('*'.repeat(80));
}

function logTI(ti) {
  console.log(ti);
  console.log('*'.repeat(80));
  console.log('Updating the First TI');
}

function logUpdateTI(ti) {
  console.log('See Updated TI Below');
  console.log('*'.repeat(80));
  console.log(ti);
  console.log('Creating TI');
  console.log('*'.repeat(80));
}

function logCreateTI(ti) {
  console.log('See Created TI Below');
  console.log('*'.repeat(80));
  console.log(ti);
  console.log('Deleting TI');
  console.log('*'.repeat(80));
}

function logDeleteTI(ti) {
  console.log('TI deleted');
  console.log('*'.repeat(80));
  console.log('Creating bulk TIs');
  console.log('*'.repeat(80));
}

function logCreateTIs(tiIndicators){
  console.log('Bulk TIs Created');
  console.log(tiIndicators);
  console.log('*'.repeat(80));
  console.log('Updating bulk TIs');
  console.log('*'.repeat(80));
}

function logUpdateTIs(tiIndicators){
  console.log('Bulk TIs Updated');
  console.log(tiIndicators);
  console.log('*'.repeat(80));
  console.log('Deleting bulk TIs');
  console.log('*'.repeat(80));
}

function logDeleteTIs(tiIndicators){
  console.log('Bulk TIs Deleted');
  console.log(tiIndicators);
  console.log('*'.repeat(80));
}

function logDeleteTIsByExternalId(tiIndicators){
  console.log('Bulk TIs Deleted by External Ids');
  console.log(tiIndicators);
  console.log('*'.repeat(80));
}

function logSecureScores(secureScores){
  console.log('Received Secure Scores');
  console.log(secureScores);
  console.log('*'.repeat(80));
}


module.exports = {
  logToken,
  logAlerts,
  logAlert,
  logUpdateAlert,
  logTIs, 
  logTI,
  logUpdateTI,
  logCreateTI, 
  logDeleteTI,
  logCreateTIs,
  logUpdateTIs, 
  logDeleteTIs,
  logDeleteTIsByExternalId,
  logSecureScores
};
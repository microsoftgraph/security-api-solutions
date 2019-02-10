const getGraphClient = require('../../util/graph');

function getAlerts(req, res, client) {
  client.api('/security/alerts').get()
    .then((alerts) => {
        res.writeHead(200, { 'Content-Type': 'application/json' });
        res.end(JSON.stringify({ alerts: alerts.value }));
    })
    .catch((err) => {
        console.error(err);
        // TODO More error handling would be needed to show better error messages
        res.writeHead(500, { 'Content-Type': 'application/json' });
        res.end(JSON.stringify({
            error: {
                code: 'internal_server_error',
                message: 'Internal server error'
            }
        }));
    });
}

const methods = {
    'GET': getAlerts
};

module.exports = (req, res) => {
  if (methods[req.method]) {
      const client = getGraphClient(req, res);

      if (!client) {
          res.writeHead(401);
          res.end(JSON.stringify({
              error: {
                  code: 'unauthorized',
                  message: 'Not authenticated'
              }
          }));
          return;
      }

      methods[req.method](req, res, client);
  } else {
    res.writeHead(405);
    res.end(JSON.stringify({
        error: {
            code: 'method_not_allowed',
            message: 'Method not supported'
        }
    }));
  }
};

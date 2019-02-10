const MicrosoftGraph = require("@microsoft/microsoft-graph-client");
const Cookies = require('cookies');
const { APIVERSION } = require('./consts');

module.exports = function getGraphClient(req, res) {
    const cookies = new Cookies(req, res);
    const token = cookies.get('token');
    if (!token) {
        return null;
    }

    return MicrosoftGraph.Client.init({
        defaultVersion: APIVERSION,
        debugLogging: true,
        authProvider: (done) => {
            //first parameter takes an error if you can't get an access token
            done(null, token);
        }
    });
};

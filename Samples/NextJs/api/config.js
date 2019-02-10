const {
    GRAPH_CLIENT_ID,
    GRAPH_CLIENT_SECRET,
    GRAPH_COOKIE_IV,
    GRAPH_COOKIE_KEY,
    GRAPH_LOGIN_REDIRECT_URL,
    GRAPH_LOGOUT_REDIRECT_URL,
    GRAPH_METADATA_URL
} = process.env;

module.exports = {
    creds: {
        redirectUrl: GRAPH_LOGIN_REDIRECT_URL,
        clientID: process.env.GRAPH_CLIENT_ID,
        clientSecret: process.env.GRAPH_CLIENT_SECRET,
        identityMetadata: process.env.GRAPH_METADATA_URL,
        allowHttpForRedirectUrl: false,
        responseType: 'code',
        validateIssuer: true,
        responseMode: 'query',
        scope: ['User.Read', 'Profile'], // permissions to request on behalf of the user
        useCookieInsteadOfSession: true,
        cookieEncryptionKeys: [{
            iv: process.env.GRAPH_COOKIE_IV,
            key: process.env.GRAPH_COOKIE_KEY
        }]
    },
    logoutRedirectUrl: GRAPH_LOGOUT_REDIRECT_URL
};

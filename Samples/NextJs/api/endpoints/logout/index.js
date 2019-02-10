const app = require('../../app');
const { logoutRedirectUrl } = require('../../config');

app.get('*', (req, res) => {
    res.clearCookie('token');
    res.clearCookie('user');
    res.redirect(`https://login.microsoftonline.com/common/oauth2/logout?post_logout_redirect_uri=${logoutRedirectUrl}`);
});

module.exports = app;

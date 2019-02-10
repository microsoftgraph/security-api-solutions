const passport = require('passport');
const app = require('../../app');

// Authentication callback.
// After we have an access token, get user data and check the scopes returned.
app.get('/token', (req, res, next) => {
    // Checks if the redirect was from the authentication or the admin consent flow.
    if (req.query.error) {
        res.redirect(`/error?err=${req.query.error}&msg=${req.query.error_description}`);
    } else if (req.query.admin_consent === "True") {
        res.redirect('/');
    } else {
        next();
    }
}, passport.authenticate('azuread-openidconnect', { failureRedirect: '/error', session: false }),
    (req, res) => {
        // TODO Should expire exactly when the token expires
        const maxAge = 60 * 60 * 1000;
        const user = {
            name: req.user.profile.displayName
        };

        res.cookie('token', req.user.accessToken, { maxAge, httpOnly: true, secure: true });
        res.cookie('user', JSON.stringify(user), { maxAge, httpOnly: false, secure: true });

        const scopesReturned = req.user.params['scope'].split(' ');

        // verify that the correct scopes are in the token
        if (scopesReturned.includes('User.Read.All') && scopesReturned.includes('SecurityEvents.ReadWrite.All')) {
            res.redirect('/');
        } else {
            res.redirect('/consent');
        }
    });

module.exports = app;

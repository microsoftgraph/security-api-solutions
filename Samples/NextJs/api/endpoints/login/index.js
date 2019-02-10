const passport = require('passport');
const app = require('../../app');

app.get('*',
    passport.authenticate('azuread-openidconnect', { failureRedirect: '/', session: false }),
    (req, res) => {
        res.redirect('/');
    });

module.exports = app;

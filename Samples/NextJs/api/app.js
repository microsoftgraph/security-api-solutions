const OIDCStrategy = require('passport-azure-ad').OIDCStrategy;
const cookieParser = require('cookie-parser');
const express = require('express');
const passport = require('passport');
const config = require('./config');

const app = express();

passport.use(new OIDCStrategy(config.creds, (iss, sub, profile, accessToken, refreshToken, params, done) => {
    done(null, {
        profile,
        accessToken,
        refreshToken,
        params
    });
  }));

app.use(cookieParser());
app.use(passport.initialize());

module.exports = app;

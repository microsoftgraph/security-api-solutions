#  A Microsoft Graph Security API example using Next.js and ZEIT Now

## Introduction

This example shows how to create a serverless [Next.js](https://nextjs.org) app
that authenticates with a Microsoft Azure Active Directory
[(AAD)](https://azure.microsoft.com/en-us/services/active-directory/) tenant and
retrieves security alerts using the
Microsoft [Graph Security](https://aka.ms/graphsecuritydocs) API.
The example is built around the
[ZEIT Now](https://zeit.co/now) deployment model, as it's utilizing Now
builders and deployment routes, but it is portable to any serverless
environment.

### Prerequisites

All the services used in this example require registering an account.

Besides creating the accounts you will also need to install one of the
ZEIT Now [clients](https://zeit.co/download) providing a command line
interface. The command line tool is required for creating deployment
secrets. A deployment can be created either by using the [GitHub integration](https://zeit.co/github) or by using [Now CLI](https://zeit.co/download#now-cli).

## Environment Variables and Secrets

In this example, all the configuration variables that need to typically be
changed are provided as environment variables and all the sensitive information
like API tokens are stored using Now secrets. The secrets are turned into secure
environment variables when the deployment is created. Therefore both are
immutable after the deployment has been created.

The `now.json` file defines the following environment variables:

| Env variable name             | Description                                                                   | Sensitive | Example                                           |
|:------------------------------|:------------------------------------------------------------------------------|:----------|:--------------------------------------------------|
| `GRAPH_CLIENT_ID`             | The application ID of your application in the AAD.                            |           | `@graph-client-id`                                |
| `GRAPH_CLIENT_SECRET`         | A client secret token created for your application by the AAD.                | X         | `@graph-secret-test`                              |
| `GRAPH_COOKIE_IV`             | Cookie encryption IV for `passport-azure-ad`.  Always 12 chars.               |           | `1234abcdef90`                                    |
| `GRAPH_COOKIE_KEY`            | Cookie encryption key for `passport-azure-ad`. Always 32 chars.               | X         | `@graph-cookie-key`                               |
| `GRAPH_LOGIN_REDIRECT_URL`    | An endpoint that continues the authentication after a token has been issued.  |
                                  Must be an FQDN + `/token` in this example.                                   |           | `https://graph.yourdomain.com/token`              |
| `GRAPH_LOGOUT_REDIRECT_URL`   | An URL where the user is redirected after logout.                             |           | `https://graph.yourdomain.com/`                   |
| `GRAPH_METADATA_URL`          | The metadata endpoint of the AAD tenant. The URL is not very sensitive,
                                  the example uses a secret called `@graph-metadata` but it's not necessary to
                                  use a secret for this URL.                                                    |           |
                                  `https://login.microsoftonline.com/your_tenant_guid/v2.0/.well-known/openid-configuration` |

See Microsoft's [Node.js example](https://github.com/microsoftgraph/nodejs-security-sample/)
for more information about setting up your application configuration in
Microsoft AAD.

The secrets are created with `now secrets add <name> <value>`. See
[Securing Environment Variables Using Secrets](https://zeit.co/docs/v2/deployments/environment-variables-and-secrets/)
for more information about using secrets in ZEIT Now.

## Deploying

At this point, you should have configured the AAD tenant and chose a domain that
you want to use for the example service. In this example, we are going to use
`graph.yourdomain.com`. This needs to be configured for the application in the
AAD tenant as well as in the Now secrets defined earlier.

Once you have configured the AAD tenant and have all the secrets created you can
try creating the deployment. Before you start, you may want to add an `alias`
property to the `now.json` file for easier aliasing. [See the documentation](https://zeit.co/docs/v2/deployments/configuration#alias) for more information on aliasing. The `alias`
property should be set to whatever you chose as a domain for the service.

Run the following command when your working directory is set to the example
directory:

```bash
$ now
```

This will create a new deployment and build the code.

If the deployment was built successfully and you added the `alias` property then
you can now run:

```bash
now alias
```

and the deployment will be aliased to the domain you chose earlier. The
deployment will be now available at `https://graph.yourdomain.com/`.

Note that the actual deployment URL returned by `now` is unusable in this
example and you will always need to alias, otherwise the authentication will
fail. This happens because AAD requires, for security, that all authentication
related URLs are preregistered in the application authentication configuration.
In a real production environment, you would want to set an alternative login URL
for testing. That could be for example `graph-test.yourdomain.com`. Doing this
allows you to make a Now alias for testing versions of your code and avoiding
disturbing the production environment while testing new changes.

## Resources

The following services, frameworks, and utilities are covered by this example:

### Services

- [Microsoft Azure Active Directory](https://azure.microsoft.com/en-us/services/active-directory/)
- [Microsoft Graph API](https://developer.microsoft.com/en-us/graph)
- [ZEIT Now](https://zeit.co/now)

### Frameworks and Utilities

- [Graph JavaScript SDK](https://github.com/microsoftgraph/msgraph-sdk-javascript)
- [Microsoft Azure Active Directory Passport.js Plug-In](https://github.com/AzureAD/passport-azure-ad)
- [Next.js](https://github.com/zeit/next.js)
- [Passport](https://github.com/jaredhanson/passport)

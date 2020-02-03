using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace FS.MinhaApi.Api.Providers
{
    public class SimpleAuthServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Acess-Control-Allow-Origin", new string[] {"*"});
            if(context.UserName != "FS" || context.Password != "FS")
            {
                context.SetError("invalid_user_or_password", "Usuario e/ou senha incorretos");
                return;
            }
            ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
            context.Validated(identity);
        }
    }
}
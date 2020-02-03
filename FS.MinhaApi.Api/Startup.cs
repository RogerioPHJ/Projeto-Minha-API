using System;
using System.Threading.Tasks;
using System.Web.Http;
using FS.MinhaApi.Api.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(FS.MinhaApi.Api.Startup))]

namespace FS.MinhaApi.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Para obter mais informações sobre como configurar seu aplicativo, visite https://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration config = new HttpConfiguration();
            ConfigureOAuth(app);
            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true, //DESATIVOU O HTTPS (NUNCA FAÇA ISSO EM PRODUÇÃO)
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(50),
                Provider = new SimpleAuthServerProvider
            };

            app.UseOAuthAuthorizationServer(oAuthOptions);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}

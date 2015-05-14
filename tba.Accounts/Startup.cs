using System;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using tba.Core.Filters;
using tba.Core.Persistence.Interfaces;
using tba.Core.Utilities;
using tba.EFPersistence;
using tba.Users.DbContext;
using tba.Users.Entities;
using tba.Users.OAuth;
using tba.Users.Services;

namespace tba.Accounts
{
    public class Startup
    {
        // This method is required by Katana:
        public void Configuration(IAppBuilder app)
        {

            // todo move to IOC
            var context = new UsersDbContext();
            IHashProvider hashProvider = new HashProvider();
            IRepository<TbaUser> repository = new EfRepository<TbaUser>(context);
            IUsersService usersService = new UsersService(repository, TimeProvider.Current, context, hashProvider);
            OAuthAuthorizationServerProvider oAuthServerProvider = new ApplicationOAuthServerProvider(usersService);
            ConfigureAuth(app, oAuthServerProvider);

            var webApiConfiguration = ConfigureWebApi();
            //LocalOnly (default), Always, Never
            webApiConfiguration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;
            app.UseWebApi(webApiConfiguration);
            UnityResolver.Register(webApiConfiguration);
        }


        private static void ConfigureAuth(IAppBuilder app, OAuthAuthorizationServerProvider oAuthServerProvider)
        {
            var oAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = oAuthServerProvider,
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),

                // Only do this for demo!!
                AllowInsecureHttp = true
            };
            app.UseOAuthAuthorizationServer(oAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }


        private static HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
            config.Filters.Add(new NotImplExceptionFilterAttribute());
            config.Filters.Add(new EntityDoesNotExistExceptionFilterAttribute());
            return config;
        }
    }
}

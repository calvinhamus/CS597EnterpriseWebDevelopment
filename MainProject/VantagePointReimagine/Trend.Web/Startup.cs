﻿using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using Trend.Web.UserProvider;

[assembly: OwinStartupAttribute(typeof(Trend.Web.Startup))]
namespace Trend.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var idProvider = new CustomUserIdProvider();

            //GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => idProvider);

            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}

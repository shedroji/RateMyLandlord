using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin;


[assembly: OwinStartupAttribute(typeof(RateMyLandlord.Startup))]
namespace RateMyLandlord
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
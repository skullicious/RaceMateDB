using System;
using Microsoft.Owin;
using Owin;


[assembly: OwinStartupAttribute(typeof(RaceMateDB.Startup))]
namespace RaceMateDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           ConfigureAuth(app);
        }

        
    }
}

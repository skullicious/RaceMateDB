using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;

namespace RaceMateDB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            //DB FIX
          //  Database.SetInitializer<RaceMateDB.Models.RaceMateDB>(null);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

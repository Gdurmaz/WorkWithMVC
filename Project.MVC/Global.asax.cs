using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Project.Core.CommonLayer;
using Project.MVC.Initializer;

namespace Project.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //When the web program leg is lifted, it automatically gives the name of the user who activated the user.
            RunCommonLayer.Common = new WebApp();

        }
    }
}

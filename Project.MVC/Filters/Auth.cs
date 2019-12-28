using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVC.Filters
{
    public class Auth : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (Models.CurrentUser.User==null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
            }
        }
    }
}
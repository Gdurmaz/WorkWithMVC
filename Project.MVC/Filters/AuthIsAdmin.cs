using Project.MVC.Models;
using System.Web.Mvc;

namespace Project.MVC.Filters
{
    public class AuthIsAdmin : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentUser.User!=null && CurrentUser.User.IsAdmin==false)
            {
                filterContext.Result = new RedirectResult("/Home/AccessBlocking");
            }
        }
    }
}
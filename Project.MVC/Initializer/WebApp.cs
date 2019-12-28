using Project.Core.CommonLayer;
using Project.Core.Entities;
using System.Web;

namespace Project.MVC.Initializer
{
    public class WebApp : ICommon
    {
        public string GetCurrentUserame()
        {
            if (HttpContext.Current.Session["login"]!=null)
            {
                var _username = HttpContext.Current.Session["login"] as BlogUser;
                return _username.Username;
            }
            else
            {
                return "System-Blog";
            }
        }
    }
}
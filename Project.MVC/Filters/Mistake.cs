using System.Web.Mvc;

namespace Project.MVC.Filters
{
    public class Mistake : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.Controller.TempData["Mistake"] = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectResult("/Home/HasError");

        }
    }
}
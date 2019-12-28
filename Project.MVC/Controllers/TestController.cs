using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Core.Connection;


namespace Project.MVC.Controllers
{
    [Mistake]
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Creating()
        {
            TestDataBase test = new TestDataBase();
            return View();
        }
    }
}
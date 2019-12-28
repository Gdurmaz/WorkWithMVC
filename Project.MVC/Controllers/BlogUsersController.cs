using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Project.Core.BusinessService;
using Project.Core.Entities;
using Project.MVC.Filters;

namespace Project.MVC.Controllers
{
    [Auth]
    [AuthIsAdmin]
    [Mistake]
    public class BlogUsersController : Controller
    {
        private UserService userService = new UserService();
        // GET: BlogUsers
        public ActionResult Index()
        {
            return View(userService.Select().OrderBy(I=>I.CreatedOn));
        }
        // GET: BlogUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogUser blogUser = userService.Find(I=>I.Id == id.Value);
            if (blogUser == null)
            {
                return HttpNotFound();
            }
            return View(blogUser);
        }
        // GET: BlogUsers/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: BlogUsers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Surname,Username,Email,Password,BirthOfDay,ProfileImageFilename,IsActive,IsAdmin,ActivateGuid,CreatedOn,ModifiedOn,ModifiedUsername")] BlogUser blogUser)
        {
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                var res_user = userService.Insert(blogUser);
                if (res_user.Errors.Count>0)
                {
                    res_user.Errors.ForEach(I => ModelState.AddModelError("", I.Message));
                    return View(blogUser);
                }
                return RedirectToAction("Index");
            }

            return View(blogUser);
        }
        // GET: BlogUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogUser blogUser = userService.Find(I => I.Id == id.Value);
            if (blogUser == null)
            {
                return HttpNotFound();
            }
            return View(blogUser);
        }
        // POST: BlogUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,Username,Email,Password,BirthOfDay,ProfileImageFilename,IsActive,IsAdmin,ActivateGuid,CreatedOn,ModifiedOn,ModifiedUsername")] BlogUser blogUser)
        {
            //Todo: Yapılacak
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                var user = userService.Update(blogUser);
                if (user.Errors.Count>0)
                {
                    user.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(blogUser);
                }
                return RedirectToAction("Index");
            }
            return View(blogUser);
        }
        // GET: BlogUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogUser blogUser = userService.Find(I => I.Id == id.Value);
            if (blogUser == null)
            {
                return HttpNotFound();
            }
            return View(blogUser);
        }
        // POST: BlogUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BlogUser user = userService.Find(I => I.Id == id);
            userService.Delete(user);
            return RedirectToAction("Index");
        }

    }
}

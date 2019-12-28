using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project.Core.BusinessService;
using Project.Core.Entities;
using Project.MVC.Filters;

namespace Project.MVC.Controllers
{
    [Auth]
    [AuthIsAdmin]
    [Mistake]
    public class CategoriesController : Controller
    {
        private CategoryServices categoryServices = new CategoryServices();

        // GET: Categories
        public ActionResult Index()
        {
            return View(categoryServices.Select().OrderBy(I=>I.ModifiedOn));
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryServices.Find(I=> I.Id==id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,CreatedOn,ModifiedOn,ModifiedUsername")] Category category)
        {
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
               categoryServices.Insert(category);
               return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryServices.Find(I=> I.Id==id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,CreatedOn,ModifiedOn,ModifiedUsername")] Category category)
        {
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                Category cat = categoryServices.Find(I => I.Id == category.Id);
                categoryServices.Update(new Category() {
                    Title =cat.Title =category.Title,
                    Description =cat.Description =category.Description
                });
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryServices.Find(I => I.Id == id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = categoryServices.Find(I => I.Id == id);
            categoryServices.Delete(category);
            return RedirectToAction("Index");
        }
    }
}

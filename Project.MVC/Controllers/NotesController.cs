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
using Project.MVC.Models;

namespace Project.MVC.Controllers
{
    [Mistake]
    public class NotesController : Controller
    {
        private NoteService noteService = new NoteService();
        private LikeService likeService = new LikeService();
        private CategoryServices categoryServices = new CategoryServices();

        // GET: Notes
        [Auth]
        public ActionResult Index()
        {
            var note = noteService.SelectQuery().Include(I => I.Category).Include(I => I.BlogUser).
                Where(I => I.BlogUser.Id == CurrentUser.User.Id).OrderByDescending(I => I.ModifiedOn);
            return View(note.ToList());
        }
        [Auth]
        public ActionResult MyLike()
        {
            var note = likeService.SelectQuery().Include(I => I.BlogUser).Include(I => I.Note).
                Where(I => I.BlogUser.Id == CurrentUser.User.Id).
                Select(I => I.Note).Include(I => I.BlogUser).Include(I => I.Category).OrderByDescending(I => I.ModifiedOn);
            return View("Index", note.ToList());
        }
        // GET: Notes/Details/5
        [Auth]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteService.Find(I => I.Id == id.Value);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }
        // GET: Notes/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(categoryServices.Select(), "Id", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Auth]
        public ActionResult Create([Bind(Include = "Id,Title,Text,IsDraft,LikeCount,CategoryID,CreatedOn,ModifiedOn,ModifiedUsername")] Note note)
        {
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                note.BlogUser = CurrentUser.User;
                noteService.Insert(note);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(categoryServices.Select(), "Id", "Title", note.CategoryID);
            return View(note);
        }
        // GET: Notes/Edit/5
        [Auth]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteService.Find(I => I.Id == id.Value);
            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(categoryServices.Select(), "Id", "Title", note.CategoryID);
            return View(note);
        }
        // POST: Notes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Auth]
        public ActionResult Edit([Bind(Include = "Id,Title,Text,IsDraft,LikeCount,CategoryID,CreatedOn,ModifiedOn,ModifiedUsername")] Note note)
        {
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                Note update = noteService.Find(I => I.Id == note.Id);
                update.Text = note.Text;
                update.Title = note.Title;
                update.IsDraft = note.IsDraft;
                update.CategoryID = note.CategoryID;
                noteService.Update(update);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(categoryServices.Select(), "Id", "Title", note.CategoryID);
            return View(note);
        }
        // GET: Notes/Delete/5
        [Auth]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteService.Find(I => I.Id == id.Value);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }
        // POST: Notes/Delete/5
        [Auth]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = noteService.Find(I => I.Id == id);
            noteService.Delete(note);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Auth]
        public ActionResult GetLiked(int[] id)
        {
            if (CurrentUser.User != null)
            {
                List<int> like = likeService.Select(I =>
                                I.BlogUser.Id == CurrentUser.User.Id && id.Contains(I.Note.Id)
                                ).Select(I => I.Note.Id).ToList();
                return Json(new { result = like });
            }
            else
            {
                return Json(new { result = new List<int>() });
            }

        }
        [HttpPost]
        [Auth]
        public ActionResult SetLikeCount(int noteid, bool liked)
        {
            if (CurrentUser.User == null)
            {
                return Json(new { hasError = true, errorMessage = "Beğenme işlemi için giriş yapmalısınız", result = 0 });
            }
            else
            {
                var like = likeService.Find(I => I.BlogUser.Id == CurrentUser.User.Id && I.Note.Id == noteid);
                var note = noteService.Find(I => I.Id == noteid);
                int _result = 0;
                if (like != null && liked == false)
                {
                    _result = likeService.Delete(like);

                }
                else if (like == null && liked == true)
                {
                    _result = likeService.Insert(new Like()
                    {
                        Note = note,
                        BlogUser = CurrentUser.User,
                    });
                }
                if (_result > 0)
                {
                    if (liked)
                    {
                        note.LikeCount++;
                    }
                    else
                    {
                        note.LikeCount--;
                    }
                    _result = noteService.Update(note);
                    if (_result>0)
                    {
                        return Json(new { hasError = false, string.Empty, _result = note.LikeCount }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new { hasError = true, errorMessage = "Beğenme işleminde hata gerçekleşti", result = note.LikeCount }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult ShowNoteDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteService.Find(I => I.Id == id.Value);
            if (note == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialNoteDetail",note);
        }
    }
}

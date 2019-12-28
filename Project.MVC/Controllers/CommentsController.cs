using Project.Core.BusinessService;
using Project.Core.Entities;
using Project.MVC.Filters;
using Project.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Project.MVC.Controllers
{
    [Mistake]
    public class CommentsController : Controller
    {
        private NoteService noteService = new NoteService();
        private CommentService commentService = new CommentService();
        // GET: Comments
        public ActionResult ShowComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var note = noteService.Find(I => I.Id == id.Value);
            if (note == null)
            {
                return HttpNotFound();
            }
            return PartialView("_PartialComments", note.Comments);
        }
        [HttpPost]
        [Auth]
        public ActionResult EditComment(int? id, string text)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comment = commentService.Find(I => I.Id == id.Value);
            if (comment == null)
            {
                return new HttpNotFoundResult();
            }
            comment.Text = text;
            if (commentService.Update(comment) > 0)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Auth]
        public ActionResult DeleteComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comment = commentService.Find(I => I.Id == id.Value);
            if (comment == null)
            {
                return new HttpNotFoundResult();
            }
            if (commentService.Delete(comment) > 0)
            {
                return Json(new { result = true }, JsonRequestBehavior.AllowGet);
        }
            return Json(new { result = false}, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Auth]
        public ActionResult InsertComment(int? note_id, Comment comment)
        {
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                if (note_id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var note = noteService.Find(I => I.Id == note_id.Value);
                if (note == null)
                {
                    return HttpNotFound();
                }
                comment.Note =note;
                comment.BlogUser = CurrentUser.User;
                if (commentService.Insert(comment)>0)
                {
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
    }
}
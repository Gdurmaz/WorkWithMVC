using Project.Core.Entities;
using Project.Core.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Project.Core.BusinessService
{
    public class CategoryServices:RepositoryBase<Category>
    {
        public List<Category> SelectWithDate()
        {
            var list = (from I in _connDb.TableCategory
                       orderby I.ModifiedOn descending
                       select I).ToList();
            return list;
        }
        /// <summary>
        /// İlişkili veri silme yöntemi - is a method deleting of relationship data.
        /// </summary>
        //private NoteService noteService;
        //private LikeService likeService;
        //private CommentService commentService;
        //public override int Delete(Category category)
        //{
        //    noteService = new NoteService();
        //    likeService = new LikeService();
        //    commentService = new CommentService();

        //    foreach (var _note in category.Notes.ToList())
        //    {
        //        foreach (var _like in _note.Likes.ToList())
        //        {
        //            likeService.Delete(_like);
        //        }
        //        foreach (var _comment in _note.Comments.ToList())
        //        {
        //            commentService.Delete(_comment);
        //        }
        //        noteService.Delete(_note);
        //    }
        //    return base.Delete(category);
        //}
    }
}

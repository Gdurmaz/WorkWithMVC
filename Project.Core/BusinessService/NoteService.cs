using Project.Core.Entities;
using Project.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Project.Core.BusinessService
{
    public class NoteService:RepositoryBase<Note>,IQuery<Note>
    {
        public IQueryable<Note> OrderBy(Expression<Func<Note, bool>> filter = null, Func<IQueryable<Note>, IOrderedQueryable<Note>> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public List<Note> SelectWithDate()
        {
            var list = (from I in _connDb.TableNote
                        orderby I.ModifiedOn descending 
                        select I).ToList();
            return list;
        }

        public IQueryable<Note> SelectQuery()
        {
            return _connDb.TableNote.AsQueryable();
        }
    }
}

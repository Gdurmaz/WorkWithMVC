using System;
using System.Linq;
using System.Linq.Expressions;
using Project.Core.Entities;
using Project.Core.Repository;

namespace Project.Core.BusinessService
{
    public class LikeService : RepositoryBase<Like>, IQuery<Like>
    {
        public IQueryable<Like> OrderBy(Expression<Func<Like, bool>> filter = null, Func<IQueryable<Like>, IOrderedQueryable<Like>> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Like> SelectQuery()
        {
            return _connDb.TableLike.AsQueryable();
        }
    }
}

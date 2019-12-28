using System;
using System.Linq;
using System.Linq.Expressions;
namespace Project.Core.Repository
{
    public interface IQuery<T> where T:class,new()
    {
        //Business Katmanında Gerektiğinde kullanmak için 
        IQueryable<T> SelectQuery();
        IQueryable<T> OrderBy(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
    }
}

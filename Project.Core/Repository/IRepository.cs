using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace Project.Core.Repository
{
    public interface IRepository<T> where T:class,new()
    {
        int Insert(T t);
        int Update(T t);
        int Delete(T t);
        int SaveChanges();
        int Control(Expression<Func<T, bool>> filter = null);
        List<T> Select();
        List<T> Select(Expression<Func<T, bool>> filter = null);
        T Find(Expression<Func<T,bool>> filter=null);
    }
}

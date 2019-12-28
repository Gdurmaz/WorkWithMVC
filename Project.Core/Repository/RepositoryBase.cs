using System;
using System.Collections.Generic;
using System.Linq;
using Project.Core.Singleton;
using Project.Core.Helper;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Project.Core.Repository
{
    public class RepositoryBase<T> :SingletonBase,IRepository<T> where T:class,new()
    {
        private DbSet<T> _table = null;
        public RepositoryBase()
        {
            _table = _connDb.Set<T>();
        }
        public int Control(Expression<Func<T, bool>> filter = null)
        {
            return filter == null ? 0 : _table.Count(filter);
        }
        public virtual int Delete(T t)
        {
            ActionMethod.DbEntityException(() => {
                if (t != null)
                {
                    _table.Remove(t);
                }
            });
            return SaveChanges();
        }
        public T Find(Expression<Func<T, bool>> filter = null)
        {
            return _table.FirstOrDefault(filter);
        }
        public int Insert(T t)
        {
            ActionMethod.DbEntityException(() => {
                if (t != null)
                {
                    if (t is Entities.CommonProp)
                    {
                        Entities.CommonProp prop = t as Entities.CommonProp;
                        prop.CreatedOn = DateTime.Now;
                        prop.ModifiedOn = DateTime.Now;
                        prop.ModifiedUsername = CommonLayer.RunCommonLayer.Common.GetCurrentUserame(); // TODO: Değiştirilecek..
                    }
                    _table.Add(t);
                }
            });
            return SaveChanges();
        }
        public int SaveChanges()
        {
            return _connDb.SaveChanges();
        }
        public List<T> Select()
        {
            return _table.ToList();
        }
        public List<T> Select(Expression<Func<T, bool>> filter = null)
        {
            return _table.Where(filter).ToList();
        }
        public int Update(T t)
        {
            ActionMethod.DbEntityException(() => {
                if (t != null)
                {
                    // Bulunan Değer üzerine gelen değerler yazılacak
                    if (t is Entities.CommonProp)
                    {
                        Entities.CommonProp prop = new Entities.CommonProp();
                        prop.ModifiedOn = DateTime.Now;
                        prop.ModifiedUsername = CommonLayer.RunCommonLayer.Common.GetCurrentUserame(); // TODO: Değiştirilecek..
                    }
                }
            });
            return SaveChanges();
        }
    }
}

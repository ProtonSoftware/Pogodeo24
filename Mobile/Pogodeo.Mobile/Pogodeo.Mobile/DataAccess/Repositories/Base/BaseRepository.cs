using Microsoft.EntityFrameworkCore;
using Pogodeo.Core;
using System;
using System.Linq;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// Base repository class to derive from by every repository in the application
    /// </summary>
    public abstract class BaseRepository<T, K> : IRepository<T, K> where T : class, IBaseObject<K>, new()
    {
        protected PogodeoMobileDbContext Db { get; set; }
         
        protected abstract DbSet<T> DbSet { get; }

        public BaseRepository(PogodeoMobileDbContext db)
        {
            Db = db;
        }

        public virtual void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Delete(K id)
        {
            var entity = new T()
            {
                Id = id,
            };
            Db.Entry(entity).State = EntityState.Deleted;
        }

        public T GetById(K id)
        {
            return DbSet.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public bool InsensitiveStringComparition(string first, string second) => string.Equals(first, second, StringComparison.OrdinalIgnoreCase);

        public OperationResult SaveChanges()
        {
            try
            {
                var changes = Db.SaveChanges();
            }
            catch(DbUpdateException ex)
            {
                if (ex.ForeginKeyViolation())
                    return new OperationResult("Foreign key violation!");

                throw ex;
            }

            return new OperationResult(true);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Pogodeo.Core;
using Pogodeo.DataAccess;
using System.Linq;

namespace Pogodeo.Services
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseRepository<T, K> : IRepository<T, K> where T : class, IBaseObject<K>, new()
    {
        protected PogodeoAppDataContext Db { get; set; }
         
        protected abstract DbSet<T> DbSet { get; }

        public BaseRepository(PogodeoAppDataContext db)
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

        public OperationResult SaveChanges()
        {
            try
            {
                var changes = Db.SaveChanges();
            }
            catch(DbUpdateException ex)
            {
                if (ex.ForeginKeyViolation())
                    return new OperationResult("Foregin key violation!");

                throw ex;
            }

            return new OperationResult(true);
        }
    }
}

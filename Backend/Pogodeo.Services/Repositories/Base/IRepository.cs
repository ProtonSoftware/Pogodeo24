using Pogodeo.DataAccess;
using System.Linq;

namespace Pogodeo.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRepository<T, K> where T : IBaseObject<K>, new()
    {
        void Add(T entity);
        void Delete(T entity);
        void Delete(K id);
        T GetById(K id);
        IQueryable<T> GetAll();
        OperationResult SaveChanges();

    }
}

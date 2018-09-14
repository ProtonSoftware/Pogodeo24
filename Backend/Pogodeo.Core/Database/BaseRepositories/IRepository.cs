using System.Linq;

namespace Pogodeo.Core
{
    /// <summary>
    /// The base repository interface to implement by every repository in the application
    /// Contains some basic functionalities to work with the database
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

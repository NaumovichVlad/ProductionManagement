using System.Linq.Expressions;

namespace DataAccess.Interfaces
{
    public interface IRepository<T>
        where T : class, IEntity
    {
        void Insert(T entity);
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);
        T GetById(int id);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);

    }
}

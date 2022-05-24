using System.Linq.Expressions;

namespace Articles.GenericRepository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null);
        Task<T> GetAsync(Expression<Func<T, bool>> expression, List<string> includes = null);
        Task InsertAsync(T entity);
        Task DeleteAsync(int id);
        void Update(T entity);
    }

}


using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
namespace Articles.GenericRepository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // todo : Get All
        Task<IList<T>> GetAllAsync(
            Expression<Func<T, bool>>? expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null
        );

        // todo : Get
        Task<T> GetAsync(
        Expression<Func<T, bool>> expression,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null
        );

        // todo : Insert
        Task InsertAsync(T entity);

        // todo : Delete
        Task DeleteAsync(int id);

        // todo : Update
        void Update(T entity);
    }
}


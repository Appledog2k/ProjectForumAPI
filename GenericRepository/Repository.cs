using System.Linq.Expressions;
using Articles.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
namespace Articles.GenericRepository
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
    public class Repository<T> : IRepository<T> where T : class
    {
        // todo : ctor
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(DatabaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // todo : Get All
        public async Task<IList<T>> GetAllAsync(
        Expression<Func<T, bool>>? expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = _dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        // todo : get article
        public async Task<T> GetAsync(
        Expression<Func<T, bool>> expression,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
            {
                query = include(query);
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        // todo : Insert
        public async Task InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

        }

        // todo : delete
        public async Task DeleteAsync(int id)
        {
            var delArticle = await _dbSet.FindAsync(id);
            _dbSet.Remove(delArticle);
        }


        // todo : Update
        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
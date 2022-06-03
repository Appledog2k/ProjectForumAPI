using Articles.Data;
using Articles.GenericRepository.IRepository;
namespace Articles.GenericRepository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IRepository<Article> _articles;
        private IRepository<Author> _authors;
        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IRepository<Article> Articles => _articles ??= new Repository<Article>(_context);
        public IRepository<Author> Authors => _authors ??= new Repository<Author>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
using Articles.Data;
namespace Articles.GenericRepository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Article> Articles { get; }
        IRepository<Author> Authors { get; }
        Task Save();
    }
}
using Domain.Entities;
using Domain.RepositoryInterfaces;
using NHibernate;
using NHibernate.Linq;

namespace Infrastructure.Repositories
{
    public class UrlRepository : IUrlRepository, IDisposable
    {
        private readonly ISession _session;
        private ITransaction _transaction;
        private bool _disposed = true;

        public UrlRepository(ISessionFactory sessionFactory)
        {
            _session = sessionFactory.OpenSession();
        }

        async Task IUrlRepository.AddAsync(ShortUrl shortUrl)
        {
            await _session.SaveAsync(shortUrl);
        }

        async Task IUrlRepository.DeleteAsync(ShortUrl shortUrl)
        {
            await _session.DeleteAsync(shortUrl);
        }

        async Task IUrlRepository.UpdateAsync(ShortUrl shortUrl)
        {
            await _session.UpdateAsync(shortUrl);
        }

        async Task<bool> IUrlRepository.ContainsAsync(string urlId)
        {
            var shortUrl = await _session.Query<ShortUrl>()
                .FirstOrDefaultAsync(url => url.ShortUrlId == urlId);

            if (shortUrl is null)
            {
                return false;
            }

            return true;
        }

        async Task<ICollection<ShortUrl>> IUrlRepository.GetAllAsync()
        {
            return await _session.Query<ShortUrl>().ToListAsync();
        }

        async Task<ShortUrl?> IUrlRepository.GetByIdAsync(Guid id)
        {
            return await _session.GetAsync<ShortUrl>(id);
        }

        async Task<ShortUrl?> IUrlRepository.GetByLongUrlAsync(Uri url)
        {
            return await _session.Query<ShortUrl>()
                .FirstOrDefaultAsync(shortUrl => shortUrl.LongUrl == url);
        }

        async Task<ShortUrl?> IUrlRepository.GetByShortUrlIdAsync(string urlId)
        {
            return await _session.Query<ShortUrl>()
                .FirstOrDefaultAsync(shortUrl => shortUrl.ShortUrlId == urlId);
        }

        void IUrlRepository.BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
            _disposed = false;
        }

        void IUrlRepository.CommitTransaction()
        {
            _transaction.Commit();
            _transaction.Dispose();
            _disposed = true;
        }

        void IUrlRepository.RollbackTransaction()
        {
            _transaction.Rollback();
            _transaction.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            if (_disposed == false && _transaction is not null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }

            if (_session is not null)
            {
                _session.Flush();
                _session.Close();
                _session.Dispose();
            }
        }
    }
}

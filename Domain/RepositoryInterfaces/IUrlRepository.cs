using Domain.Entities;

namespace Domain.RepositoryInterfaces
{
    public interface IUrlRepository
    {
        Task<ICollection<ShortUrl>> GetAllAsync();
        Task<ShortUrl?> GetByIdAsync(Guid id);
        Task<ShortUrl?> GetByLongUrlAsync(Uri url);
        Task<ShortUrl?> GetByShortUrlIdAsync(string urlId);
        Task<bool> ContainsAsync(string urlId);
        Task AddAsync(ShortUrl shortUrl);
        Task UpdateAsync(ShortUrl shortUrl);
        Task DeleteAsync(ShortUrl shortUrl);
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}

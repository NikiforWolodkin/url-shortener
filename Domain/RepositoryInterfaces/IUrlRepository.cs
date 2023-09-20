using Domain.Entities;

namespace Domain.RepositoryInterfaces
{
    public interface IUrlRepository
    {
        Task<ICollection<ShortUrl>> GetShortUrlsAsync(int page);
        Task<ShortUrl?> GetShortUrlByIdAsync(Guid id);
        Task<ShortUrl?> GetShortUrlByLongUrlAsync(Uri url);
        Task<ShortUrl?> GetShortUrlByShortUrlIdAsync(string urlId);
        Task<bool> ContainsShortUrlAsync(string urlId); 
        Task AddShortUrlAsync(ShortUrl shortUrl);
        void DeleteShortUrl(ShortUrl shortUrl);
        Task SaveChangesAsync();
    }
}

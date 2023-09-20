using Domain.Entities;

namespace Services.Interfaces
{
    public interface IUrlService
    {
        Task<ICollection<ShortUrl>> GetShortUrlsAsync(int page);
        Task<ShortUrl?> GetShortUrlByIdAsync(Guid id);
        Task<ShortUrl?> GetShortUrlByLongUrlAsync(Uri url);
        Task<ShortUrl?> GetShortUrlByShortUrlIdAsync(string urlId);
        Task<ShortUrl> ShortenUrlAsync(Uri url);
        Task IncrementUrlClickCount(ShortUrl shortUrl);
        Task DeleteShortUrlAsync(ShortUrl shortUrl);
    }
}

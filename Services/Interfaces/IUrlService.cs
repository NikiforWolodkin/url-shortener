using Domain.Entities;

namespace Services.Interfaces
{
    public interface IUrlService
    {
        Task<ICollection<ShortLongUrlPair>> GetShortUrlsAsync(int page);
        Task<ShortLongUrlPair?> GetShortUrlAsync(Uri uri);
        Task<ShortLongUrlPair> ShortenUrlAsync(Uri uri);
        Task IncrementUrlClickCount(ShortLongUrlPair urlPair);
        Task DeleteShortUrlAsync(ShortLongUrlPair urlPair);
    }
}

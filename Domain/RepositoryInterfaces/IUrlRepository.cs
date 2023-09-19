using Domain.Entities;

namespace Domain.RepositoryInterfaces
{
    public interface IUrlRepository
    {
        Task<ICollection<ShortLongUrlPair>> GetShortUrlsAsync(int page);
        Task<ShortLongUrlPair?> GetShortUrlAsync(Uri uri);
        Task<ShortLongUrlPair> AddShortUrlAsync(ShortLongUrlPair urlPair);
        Task DeleteShortUrlAsync(ShortLongUrlPair urlPair);
        Task SaveChangesAsync();
    }
}

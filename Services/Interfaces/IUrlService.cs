using Domain.Entities;
using Services.Dtos;

namespace Services.Interfaces
{
    public interface IUrlService
    {
        Task<ICollection<ShortUrlDto>> GetShortUrlsAsync(int page);
        Task<ShortUrlDto?> GetShortUrlByIdAsync(Guid id);
        Task<ShortUrlDto?> GetShortUrlByLongUrlAsync(Uri url);
        Task<ShortUrlDto?> GetShortUrlByShortUrlIdAsync(string urlId);
        Task<ShortUrlDto> ShortenUrlAsync(Uri url);
        Task IncrementUrlClickCount(Guid id);
        Task DeleteShortUrlAsync(Guid id);
    }
}

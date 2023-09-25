using Domain.Entities;
using Services.Dtos;

namespace Services.Interfaces
{
    public interface IUrlService
    {
        Task<ICollection<ShortUrlDto>> GetAllAsync(int page);
        Task<ShortUrlDto?> GetByIdAsync(Guid id);
        Task<ShortUrlDto?> GetByLongUrlAsync(Uri url);
        Task<ShortUrlDto?> GetByShortUrlIdAsync(string urlId);
        Task<ShortUrlDto> ShortenUrlAsync(Uri url);
        Task IncrementUrlClickCount(Guid id);
        Task DeleteAsync(Guid id);
    }
}

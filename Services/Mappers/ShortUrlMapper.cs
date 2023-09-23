using Domain.Entities;
using Services.Dtos;

namespace Services.Mappers
{
    public class ShortUrlMapper
    {
        private readonly Uri _hostUrl;

        public ShortUrlMapper(Uri hostUrl)
        {
            _hostUrl = hostUrl;
        }

        public ShortUrlDto? ToDto(ShortUrl? shortUrl)
        {
            if (shortUrl is null)
            {
                return null;
            }    

            return new ShortUrlDto
            {
                Id = shortUrl.Id,
                LongUrl = shortUrl.LongUrl,
                ShortUrl = new Uri(_hostUrl.ToString() + shortUrl.ShortUrlId),
                ClickCount = shortUrl.ClickCount,
                CreationTime = shortUrl.CreationTime
            };
        }

        public ICollection<ShortUrlDto> ToDto(ICollection<ShortUrl> shortUrls)
        {
            return shortUrls
                .Select(shortUrl => ToDto(shortUrl))
                .ToList();
        }
    }
}

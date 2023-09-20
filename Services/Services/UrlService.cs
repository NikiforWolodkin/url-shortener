using Domain.Entities;
using Domain.RepositoryInterfaces;
using Services.Interfaces;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Services.Services
{
    public class UrlService : IUrlService
    {
        private const string Base62CharSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private readonly IUrlRepository _urlRepository;

        public UrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        async Task IUrlService.DeleteShortUrlAsync(ShortUrl shortUrl)
        {
            _urlRepository.DeleteShortUrl(shortUrl);

            await _urlRepository.SaveChangesAsync();
        }

        async Task<ShortUrl?> IUrlService.GetShortUrlByLongUrlAsync(Uri url)
        {
            return await _urlRepository.GetShortUrlByLongUrlAsync(url);
        }

        async Task<ICollection<ShortUrl>> IUrlService.GetShortUrlsAsync(int page)
        {
            return await _urlRepository.GetShortUrlsAsync(page);
        }

        async Task IUrlService.IncrementUrlClickCount(ShortUrl shortUrl)
        {
            shortUrl.ClickCount++;

            await _urlRepository.SaveChangesAsync();
        }

        async Task<ShortUrl> IUrlService.ShortenUrlAsync(Uri url)
        {
            var existingShortUrl = await _urlRepository.GetShortUrlByLongUrlAsync(url);

            if (existingShortUrl is not null)
            {
                return existingShortUrl;
            }    

            var shortUrlId = await CreateShortUrlId(url);

            var shortUrl = new ShortUrl
            {
                LongUrl = url,
                ShortUrlId = shortUrlId,
                ClickCount = 0,
                CreationTime = DateTime.UtcNow
            };

            await _urlRepository.AddShortUrlAsync(shortUrl);

            await _urlRepository.SaveChangesAsync();

            return shortUrl;
        }

        async Task<ShortUrl?> IUrlService.GetShortUrlByShortUrlIdAsync(string urlId)
        {
            return await _urlRepository.GetShortUrlByShortUrlIdAsync(urlId);
        }

        async Task<ShortUrl?> IUrlService.GetShortUrlByIdAsync(Guid id)
        {
            return await _urlRepository.GetShortUrlByIdAsync(id);
        }

        private async Task<string> CreateShortUrlId(Uri url)
        {
            using var sha256 = SHA256.Create();

            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(url.ToString()));

            var intRepresentation = BigInteger.Abs(new BigInteger(bytes.Reverse().ToArray()));
            var hash = ToBase62(intRepresentation).Substring(0, 7);

            while (await _urlRepository.ContainsShortUrlAsync(hash))
            {
                hash = IncrementChars(hash);
            }

            return hash;
        }

        private static string ToBase62(BigInteger value)
        {
            var result = new StringBuilder();

            while (value > 0)
            {
                result.Insert(0, Base62CharSet[(int)(value % 62)]);
                value /= 62;
            }

            return result.ToString();
        }

        private static string IncrementChars(string value)
        {
            var chars = value.ToCharArray();

            for (var i = chars.Length - 1; i >= 0; i--)
            {
                var index = Base62CharSet.IndexOf(chars[i]);

                if (index < Base62CharSet.Length - 1)
                {
                    chars[i] = Base62CharSet[index + 1];
                    break;
                }
            }

            return new string(chars);
        }
    }
}

using Domain.Entities;
using Domain.RepositoryInterfaces;
using Services.Dtos;
using Services.Interfaces;
using Services.Mappers;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Services.Services
{
    public class UrlService : IUrlService
    {
        private const string Base62CharSet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private readonly IUrlRepository _urlRepository;
        private readonly ShortUrlMapper _mapper = new (new Uri("https://localhost:7054"));

        public UrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        async Task IUrlService.DeleteShortUrlAsync(Guid id)
        {
            _urlRepository.BeginTransaction();

            var shortUrl = await _urlRepository.GetByIdAsync(id);

            if (shortUrl is not null)
            {
                await _urlRepository.DeleteAsync(shortUrl);
            }

            _urlRepository.CommitTransaction();
        }

        async Task<ShortUrlDto?> IUrlService.GetShortUrlByLongUrlAsync(Uri url)
        {
            var shortUrl = await _urlRepository.GetByLongUrlAsync(url);

            return _mapper.ToDto(shortUrl);
        }

        async Task<ICollection<ShortUrlDto>> IUrlService.GetShortUrlsAsync(int page)
        {
            var shortUrls = await _urlRepository.GetAllAsync();

            return _mapper.ToDto(shortUrls);
        }

        async Task IUrlService.IncrementUrlClickCount(Guid id)
        {
            _urlRepository.BeginTransaction();

            var shortUrl = await _urlRepository.GetByIdAsync(id);

            if (shortUrl is not null)
            {
                shortUrl.ClickCount++;

                await _urlRepository.UpdateAsync(shortUrl);
            }

            _urlRepository.CommitTransaction();
        }

        async Task<ShortUrlDto> IUrlService.ShortenUrlAsync(Uri url)
        {
            _urlRepository.BeginTransaction();

            var existingShortUrl = await _urlRepository.GetByLongUrlAsync(url);

            if (existingShortUrl is not null)
            {
                _urlRepository.CommitTransaction();

                return _mapper.ToDto(existingShortUrl);
            }    

            var shortUrlId = await CreateShortUrlId(url);

            var shortUrl = new ShortUrl
            {
                LongUrl = url,
                ShortUrlId = shortUrlId,
                ClickCount = 0,
                CreationTime = DateTime.UtcNow
            };

            await _urlRepository.AddAsync(shortUrl);

            _urlRepository.CommitTransaction();

            return _mapper.ToDto(shortUrl);
        }

        async Task<ShortUrlDto?> IUrlService.GetShortUrlByShortUrlIdAsync(string urlId)
        {
            var shortUrl = await _urlRepository.GetByShortUrlIdAsync(urlId);

            return _mapper.ToDto(shortUrl);
        }

        async Task<ShortUrlDto?> IUrlService.GetShortUrlByIdAsync(Guid id)
        {
            var shortUrl = await _urlRepository.GetByIdAsync(id);

            return _mapper.ToDto(shortUrl);
        }

        private async Task<string> CreateShortUrlId(Uri url)
        {
            using var sha256 = SHA256.Create();

            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(url.ToString()));

            var intRepresentation = BigInteger.Abs(new BigInteger(bytes.Reverse().ToArray()));
            var hash = ToBase62(intRepresentation).Substring(0, 7);

            while (await _urlRepository.ContainsAsync(hash))
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

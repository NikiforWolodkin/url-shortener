using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private const int PageSize = 50;
        private readonly DataContext _context;

        public UrlRepository(DataContext context)
        {
            _context = context;
        }

        async Task<ShortUrl?> IUrlRepository.GetShortUrlByShortUrlIdAsync(string urlId)
        {
            return await _context.ShortUrls
                .FirstOrDefaultAsync(shortUrl => shortUrl.ShortUrlId == urlId);
        }

        async Task IUrlRepository.AddShortUrlAsync(ShortUrl shortUrl)
        {
            await _context.ShortUrls.AddAsync(shortUrl);
        }

        async Task<bool> IUrlRepository.ContainsShortUrlAsync(string urlId)
        {
            var url = await _context.ShortUrls
                .FirstOrDefaultAsync(shortUrl => shortUrl.ShortUrlId == urlId);

            if (url is null)
            {
                return false;
            }

            return true;
        }

        void IUrlRepository.DeleteShortUrl(ShortUrl shortUrl)
        {
            _context.ShortUrls.Remove(shortUrl);
        }

        async Task<ShortUrl?> IUrlRepository.GetShortUrlByLongUrlAsync(Uri url)
        {
            return await _context.ShortUrls
                .FirstOrDefaultAsync(shortUrl => shortUrl.LongUrl == url);
        }

        async Task<ICollection<ShortUrl>> IUrlRepository.GetShortUrlsAsync(int page)
        {
            return await _context.ShortUrls
                .Skip(page * PageSize)
                .Take(PageSize)
                .ToListAsync();
        }

        async Task IUrlRepository.SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        async Task<ShortUrl?> IUrlRepository.GetShortUrlByIdAsync(Guid id)
        {
            return await _context.ShortUrls
                .FirstOrDefaultAsync(shortUrl => shortUrl.Id == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace Web.Controllers
{
    public class UrlController : Controller
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        public async Task<IActionResult> GetShortUrlsAsync(int page = 0)
        {
            var urlPairs = await _urlService.GetShortUrlsAsync(page);

            return Ok(urlPairs);
        }

        [ActionName("GetShortUrlAsync")]
        public async Task<IActionResult> GetShortUrlAsync(Uri uri)
        {
            var urlPair = await _urlService.GetShortUrlAsync(uri);

            if (urlPair is null)
            { 
                return NotFound();
            }

            return Ok(urlPair);
        }

        public async Task<IActionResult> ShortenUrl(Uri uri)
        {
            var urlPair = await _urlService.ShortenUrlAsync(uri);

            return CreatedAtAction("GetShortUrlAsync", urlPair);
        }
    }
}

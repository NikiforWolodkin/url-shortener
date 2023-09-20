using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Web.Controllers
{
    [Route("api")]
    public class UrlController : Controller
    {
        private readonly IUrlService _urlService;

        public UrlController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet("short-urls")]
        public async Task<IActionResult> GetShortUrlsAsync(int page = 0)
        {
            var shortUrls = await _urlService.GetShortUrlsAsync(page);

            return Ok(shortUrls);
        }

        [HttpGet("short-urls/{id:Guid}")]
        [ActionName("GetShortUrlAsync")]
        public async Task<IActionResult> GetShortUrlAsync(Guid id)
        {
            var shortUrl = await _urlService.GetShortUrlByIdAsync(id);

            if (shortUrl is null)
            { 
                return NotFound();
            }

            return Ok(shortUrl);
        }

        [HttpPost("short-urls")]
        public async Task<IActionResult> ShortenUrl([FromBody] Uri url)
        {
            var shortUrl = await _urlService.ShortenUrlAsync(url);

            return CreatedAtAction(nameof(GetShortUrlAsync), new { url = shortUrl.LongUrl }, shortUrl);
        }

        [HttpDelete("short-urls/{id:Guid}")]
        public async Task<IActionResult> DeleteShortUrl(Guid id)
        {
            var shortUrl = await _urlService.GetShortUrlByIdAsync(id);

            if (shortUrl is null)
            {
                return NotFound();
            }

            await _urlService.DeleteShortUrlAsync(shortUrl);

            return NoContent();
        }
    }
}

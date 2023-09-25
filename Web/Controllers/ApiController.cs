using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Web.Models;
using Web.Validators;

namespace Web.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly IUrlService _urlService;

        public ApiController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet("short-urls")]
        public async Task<IActionResult> GetShortUrlsAsync(int page = 0)
        {
            var shortUrls = await _urlService.GetAllAsync(page);

            return Ok(shortUrls);
        }

        [HttpGet("short-urls/{id:Guid}")]
        public async Task<IActionResult> GetShortUrlAsync(Guid id)
        {
            var shortUrl = await _urlService.GetByIdAsync(id);

            if (shortUrl is null)
            { 
                return NotFound();
            }

            return Ok(shortUrl);
        }

        [HttpPost("short-urls")]
        public async Task<IActionResult> ShortenUrl(ShortenLinkRequestModel requestModel)
        {
            if (!UrlValidator.IsValidUrl(requestModel.Url))
            {
                return BadRequest("URL is invalid.");
            }

            var shortUrl = await _urlService.ShortenUrlAsync(requestModel.Url);

            return CreatedAtAction("GetShortUrl", new { id = shortUrl.Id }, shortUrl);
        }

        [HttpDelete("short-urls/{id:Guid}")]
        public async Task<IActionResult> DeleteShortUrl(Guid id)
        {
            await _urlService.DeleteAsync(id);

            return NoContent();
        }
    }
}

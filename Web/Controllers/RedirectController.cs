using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Web.Controllers
{
    public class RedirectController : Controller
    {
        private readonly IUrlService _urlService;

        public RedirectController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet("{urlId}")]
        public async Task<IActionResult> RedirectAsync(string urlId)
        {
            var shortUrl = await _urlService.GetShortUrlByShortUrlIdAsync(urlId);

            if (shortUrl is not null)
            {
                await _urlService.IncrementUrlClickCount(shortUrl.Id);

                return Redirect(shortUrl.LongUrl.ToString());
            }

            return NotFound();
        }
    }
}

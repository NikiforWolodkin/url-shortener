using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
    [Route("app")]
    public class AppController : Controller
    {
        private readonly IUrlService _urlService;

        public AppController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet("index")]
        public async Task<IActionResult> IndexAsync()
        {
            var shortUrls = await _urlService.GetShortUrlsAsync(0);

            var indexViewModel = new IndexViewModel
            {
                ShortUrls = shortUrls.ToList(),
            };

            return View(indexViewModel);
        }

        [HttpGet("shorten-link")]
        public async Task<IActionResult> ShortenLinkAsync()
        {
            return View();
        }

        [HttpPost("shorten")]
        public async Task<IActionResult> ShortenLinkAsync(ShortenLinkRequestModel requestModel)
        {
            await _urlService.ShortenUrlAsync(requestModel.Url);

            return RedirectToAction("index");
        }

        [HttpPost("delete/{id:Guid}")]
        public async Task<IActionResult> DeleteShortUrlAsync(Guid id)
        {
            await _urlService.DeleteShortUrlAsync(id);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
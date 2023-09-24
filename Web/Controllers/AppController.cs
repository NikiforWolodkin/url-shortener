using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Diagnostics;
using Web.Models;
using Web.Validators;

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

            var indexViewModel = new IndexViewModel(shortUrls.ToList());

            return View(indexViewModel);
        }

        [HttpGet("create-short-link")]
        public async Task<IActionResult> CreateShortLinkAsync(string? url, bool? isValid)
        {
            var shortenLinkViewModel = new ShortenLinkViewModel(url, isValid);

            return View(shortenLinkViewModel);
        }

        [HttpPost("shorten")]
        public async Task<IActionResult> ShortenLinkAsync(ShortenLinkRequestModel requestModel)
        {
            if (!UrlValidator.IsValidUrl(requestModel.Url))
            {
                return RedirectToAction("createshortlink", new { url = requestModel.Url.ToString(), isValid = false });
            }

            await _urlService.ShortenUrlAsync(requestModel.Url);

            return RedirectToAction("index");
        }

        [HttpPost("delete/{id:Guid}")]
        public async Task<IActionResult> DeleteShortUrlAsync(Guid id)
        {
            await _urlService.DeleteShortUrlAsync(id);

            return RedirectToAction("Index");
        }
    }
}
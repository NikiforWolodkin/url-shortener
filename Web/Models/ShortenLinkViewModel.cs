using Domain.Entities;
using Services.Dtos;

namespace Web.Models
{
    public class ShortenLinkViewModel 
    {
        public string Url { get; set; } = string.Empty;
        public bool IsValid { get; set; } = true;

        public ShortenLinkViewModel(string? url, bool? isValid)
        {
            if (url is not null)
            {
                Url = url;
            }

            if (isValid is not null)
            {
                IsValid = (bool)isValid;
            }
        }
    }
}

using Domain.Entities;
using Services.Dtos;

namespace Web.Models
{
    public class IndexViewModel 
    {
        public List<ShortUrlDto> ShortUrls { get; set; }
    }
}

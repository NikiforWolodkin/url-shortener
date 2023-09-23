using Domain.Entities;

namespace Services.Dtos
{
    public class ShortUrlDto
    {
        public Guid Id { get; set; }
        public Uri LongUrl { get; set; }
        public Uri ShortUrl { get; set; }
        public int ClickCount { get; set; }
        public DateTime CreationTime { get; set; }
    }
}

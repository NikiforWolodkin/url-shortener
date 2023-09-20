namespace Domain.Entities
{
    public class ShortUrl
    {
        public Guid Id { get; set; }
        public Uri LongUrl { get; set; }
        public string ShortUrlId { get; set; }
        public int ClickCount { get; set; }
        public DateTime CreationTime { get; set; }
    }
}

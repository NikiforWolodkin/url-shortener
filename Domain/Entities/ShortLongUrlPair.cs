namespace Domain.Entities
{
    public class ShortLongUrlPair
    {
        public Uri LongUrl { get; set; }
        public Uri ShortUrl { get; set; }
        public int ClickCount { get; set; }
        public DateTime CreationTime { get; set; }
    }
}

namespace Domain.Entities
{
    public class ShortUrl
    {
        public virtual Guid Id { get; set; }
        public virtual Uri LongUrl { get; set; }
        public virtual string ShortUrlId { get; set; }
        public virtual int ClickCount { get; set; }
        public virtual DateTime CreationTime { get; set; }
    }
}

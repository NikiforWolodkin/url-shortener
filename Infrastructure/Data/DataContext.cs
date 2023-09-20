using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DbSet<ShortUrl> ShortUrls { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

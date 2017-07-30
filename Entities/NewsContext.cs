using Microsoft.EntityFrameworkCore;

namespace NewsTicker.Entities
{
    public class NewsContext : DbContext
    {
        public DbSet<NewsEvent> Events { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=news.db");
        }
    }
}
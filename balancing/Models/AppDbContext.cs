using Microsoft.EntityFrameworkCore;

namespace balancing.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Flow> Flows { get; set; }
    }
}
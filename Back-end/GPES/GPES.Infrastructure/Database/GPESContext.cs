using GPES.Domain.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace GPES.Infrastructure.Database
{
    public class GPESContext : DbContext
    {
        public GPESContext(DbContextOptions<GPESContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
        }
    }
}

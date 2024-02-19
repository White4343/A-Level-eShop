using Microsoft.EntityFrameworkCore;
using Order.API.Data.EntityConfigurations;

namespace Order.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Order> Orders { get; set; } = null!;
        public DbSet<Entities.OrderBasket> OrderBaskets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderBasketEntityConfiguration());
        }
    }
}

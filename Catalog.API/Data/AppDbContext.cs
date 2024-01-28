using Catalog.API.Data.EntityConfigurations;
using Type = Catalog.API.Data.Entities.Type;

namespace Catalog.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; } = null!;

        public DbSet<Brand> Brands { get; set; } = null!;

        public DbSet<Type> Types { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BrandEntityConfigurations());
            modelBuilder.ApplyConfiguration(new TypeEntityConfigurations());
            modelBuilder.ApplyConfiguration(new ItemEntityConfigurations());
        }
    }
}

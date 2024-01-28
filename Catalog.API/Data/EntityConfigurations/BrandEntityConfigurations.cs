namespace Catalog.API.Data.EntityConfigurations
{
    public class BrandEntityConfigurations : IEntityTypeConfiguration<Brand>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .UseHiLo("brands_hilo")
                .IsRequired();

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}

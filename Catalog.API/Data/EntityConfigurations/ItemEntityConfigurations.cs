using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.API.Data.EntityConfigurations
{
    public class ItemEntityConfigurations : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .UseHiLo("itemseq");

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(i => i.PictureUrl);

            builder.Property(i => i.Description);

            builder.Property(i => i.AvailableStock)
                .IsRequired();

            builder.HasOne(i => i.Type)
                .WithMany()
                .HasForeignKey(i => i.TypeId);

            builder.HasOne(i => i.Brand)
                .WithMany()
                .HasForeignKey(i => i.BrandId);
        }
    }
}

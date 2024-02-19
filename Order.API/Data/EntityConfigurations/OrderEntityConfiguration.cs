using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.API.Data.EntityConfigurations
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Entities.Order>
    {
        public void Configure(EntityTypeBuilder<Entities.Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .UseHiLo("orderseq")
                .IsRequired();

            builder.Property(o => o.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.CreatedAt)
                .IsRequired();

            builder.Property(o => o.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.UserLogin)
                .IsRequired();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.API.Data.EntityConfigurations
{
    public class OrderBasketEntityConfiguration : IEntityTypeConfiguration<Entities.OrderBasket>
    {
        public void Configure(EntityTypeBuilder<Entities.OrderBasket> builder)
        {
            builder.ToTable("OrderBaskets");

            builder.HasKey(ob => ob.Id);

            builder.Property(ob => ob.Id)
                .UseHiLo("orderbasketsseq")
                .IsRequired();

            builder.Property(ob => ob.CreatedAt)
                .IsRequired();

            builder.Property(ob => ob.Quantity)
                .IsRequired();

            builder.Property(ob => ob.ItemPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(ob => ob.ItemId)
                .IsRequired();

            builder.HasOne(ob => ob.Order)
                .WithMany()
                .HasForeignKey(ob => ob.OrderId);
        }
    }
}

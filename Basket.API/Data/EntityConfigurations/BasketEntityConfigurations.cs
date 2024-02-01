namespace Basket.API.Data.EntityConfigurations
{
    public class BasketEntityConfigurations : IEntityTypeConfiguration<Entities.Basket>
    {
        public void Configure(
            Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.Basket> builder)
        {
            builder.ToTable("Baskets");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .UseHiLo("baskets_hilo")
                .IsRequired();

            builder.Property(b => b.CreatedAt)
                .IsRequired();

            builder.Property(b => b.Quantity)
                .IsRequired();

            builder.Property(b => b.ItemPrice)
                .IsRequired();

            builder.Property(b => b.ItemId)
                .IsRequired();

            builder.Property(b => b.UserLogin)
                .IsRequired();
        }
    }
}

using Type = Catalog.API.Data.Entities.Type;

namespace Catalog.API.Data.EntityConfigurations
{
    public class TypeEntityConfigurations : IEntityTypeConfiguration<Type>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Type> builder)
        {
            builder.ToTable("Types");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .UseHiLo("types_hilo")
                .IsRequired();

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pharmacy_managment.Database.ModelConfigration
{
    public class SupplierConfigration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
           builder.HasKey(t => t.Id);
           builder.Property(t => t.Name).HasMaxLength(100).IsRequired();
           builder.Property(t => t.Email).HasMaxLength(100).IsRequired();
           builder.Property(t => t.Phone).HasMaxLength(100).IsRequired();
           builder.HasIndex(t=>t.Email).IsUnique();
        }
    }
}

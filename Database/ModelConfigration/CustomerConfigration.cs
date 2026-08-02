using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pharmacy_managment.Database.ModelConfigration
{
    public class CustomerConfigration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c=>c.Id);
            builder.Property(c=>c.FullName).HasMaxLength(100).IsRequired();
            builder.Property(t => t.PhoneNumber).HasMaxLength(100).IsRequired();
            builder.Property(c=>c.Address).HasMaxLength(200).IsRequired();
        }
    }
}

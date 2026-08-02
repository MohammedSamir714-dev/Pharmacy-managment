using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pharmacy_managment.Database.ModelConfigration
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.InvoiceDate).IsRequired();

            builder.Property(i => i.Paymentmethod)
                .HasConversion<int>()
                .IsRequired();
            builder.Ignore(i => i.TotalAmount);

            builder.HasOne(i => i.Customer)
            .WithMany(c => c.invoices)
            .HasForeignKey(i => i.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.Pharmacist)
                .WithMany(p => p.inovices)
                .HasForeignKey(i => i.PharmacistId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

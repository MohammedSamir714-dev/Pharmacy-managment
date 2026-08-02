using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pharmacy_managment.Database.ModelConfigration
{
    public class InvoiceMedicenedetailsConfigration : IEntityTypeConfiguration<InvoiceMedicenedetails>
    {
        public void Configure(EntityTypeBuilder<InvoiceMedicenedetails> builder)
        {
            builder.HasKey(d => new { d.InvoiceId, d.MediceneId });

            builder.Property(d => d.Quantity).IsRequired();
            builder.Property(d => d.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();

            builder.HasOne(d => d.Invoice)
                .WithMany(i => i.InvoiceMedicenedetails)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasOne(d => d.Medicene)
                .WithMany(m => m.InvoiceMedicenedetails)
                .HasForeignKey(d => d.MediceneId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.ToTable(t => t.HasCheckConstraint("CK_InvoiceDetail_Quantity", "[Quantity] > 0"));
        }
    }
}

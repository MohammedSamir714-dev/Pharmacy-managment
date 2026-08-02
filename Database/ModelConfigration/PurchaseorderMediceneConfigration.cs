using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pharmacy_managment.Database.ModelConfigration
{
    public class PurchaseorderMediceneConfigration : IEntityTypeConfiguration<PurchaseOrderMedicene>
    {

        public void Configure(EntityTypeBuilder<PurchaseOrderMedicene> builder)
        {
           builder.HasKey(m=>new {m.PurchaseOrderId,m.MediceneId});
           builder.Property(m=>m.Quantity).IsRequired();
            builder.Property(x => x.UnitPrice)
         .HasPrecision(18, 2);
            builder.ToTable(t => t.HasCheckConstraint("CK_PurchaseOrderDetail_Quantity", "[Quantity] > 0"));
            builder.HasOne(d=>d.PurChaseOrder).WithMany(p=>p.PurchaseOrderMedicenes).HasForeignKey(p=>p.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(d => d.Medicene).WithMany(p => p.PurchaseOrderMedicenes).HasForeignKey(p => p.MediceneId)
              .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

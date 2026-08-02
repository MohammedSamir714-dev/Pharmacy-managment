using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pharmacy_managment.Database.ModelConfigration
{
    public class PurchaseOrderConfigration : IEntityTypeConfiguration<PurChaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurChaseOrder> builder)
        {
            builder.HasKey(p=>p.Id);
            builder.Property(p=>p.orderDate).IsRequired();
            builder.Property(x => x.PurchaseOrderStatus)
          .HasConversion<int>()
          .IsRequired();
            builder.HasOne(p=>p.Supplier).WithMany(p=>p.purChaseOrders).HasForeignKey(p=>p.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

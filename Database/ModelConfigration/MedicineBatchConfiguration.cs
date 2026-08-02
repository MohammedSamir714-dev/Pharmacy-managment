using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pharmacy_managment.Database.ModelConfigration
{
    public class MedicineBatchConfiguration : IEntityTypeConfiguration<MedicineBatch>
    {
        public void Configure(EntityTypeBuilder<MedicineBatch> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.BatchName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(x => x.BatchName)
                .IsUnique();

            builder.Property(x => x.QuntityReceived)
                .IsRequired();

            builder.Property(x => x.Quntityremaining)
                .IsRequired();

            builder.Property(x => x.ManufactureDate)
                .IsRequired();

            builder.Property(x => x.ExpiryDate)
                .IsRequired();

            builder.Property(x => x.RowVersion)
                .IsRowVersion();

            builder.HasOne(x => x.Medicene)
                .WithMany(x => x.medicineBatches)
                .HasForeignKey(x => x.MediceneId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PurchaseOrder)
                .WithMany(x => x.MedicineBatches)
                .HasForeignKey(x => x.PurchaseOrderId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pharmacy_managment.Database.ModelConfigration
{
    public class MedicineConfigration : IEntityTypeConfiguration<Medicene>
    {
        public void Configure(EntityTypeBuilder<Medicene> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x=>x.Price).HasColumnType("decimal(18,2)").IsRequired();
       
            builder.HasOne(m=>m.Category).WithMany(m=>m.medicenes).HasForeignKey(m=>m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

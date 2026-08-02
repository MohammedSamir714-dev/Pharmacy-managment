using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pharmacy_managment.Database.ModelConfigration
{
    public class PharmsistConfigration : IEntityTypeConfiguration<Pharmacist>
    {
        public void Configure(EntityTypeBuilder<Pharmacist> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Licensenumber).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Salary).HasColumnType("decimal(18,2)").IsRequired();


            builder.Property(x => x.Status)
                .HasConversion<int>();

            builder.HasIndex(p => p.Licensenumber).IsUnique(); 

            builder.HasOne(p => p.ApplicationUser)
                .WithOne(u => u.Pharmacist)
                .HasForeignKey<Pharmacist>(p => p.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(p => p.ApplicationUserId).IsUnique();
        }
    }
}

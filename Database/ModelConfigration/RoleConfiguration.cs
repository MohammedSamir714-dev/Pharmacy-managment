using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy_managment.Abstractions.Consts;

namespace Pharmacy_managment.Database.ModelConfigration
{
    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(
                new ApplicationRole
                {
                    Id = DefaultRoles.AdminRoleId,
                    Name = DefaultRoles.Admin,
                    NormalizedName = DefaultRoles.Admin.ToUpper(),
                    ConcurrencyStamp = DefaultRoles.AdminRoleConcurrencyStamp
                },
                new ApplicationRole
                {
                    Id = DefaultRoles.PharmacistRoleId,
                    Name = DefaultRoles.Pharmacist,
                    NormalizedName = DefaultRoles.Pharmacist.ToUpper(),
                    ConcurrencyStamp = DefaultRoles.PharmacistRoleConcurrencyStamp
                }
               
            );
        }
    }
}

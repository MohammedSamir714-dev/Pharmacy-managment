using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy_managment.Abstractions.Consts;

namespace Pharmacy_managment.Database.ModelConfigration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            builder.HasData(new ApplicationUser
            {
                Id = DefaultUsers.AdminId,
                FirstName = DefaultUsers.AdminFirstName,
                LastName = DefaultUsers.AdminLastName,
                Email = DefaultUsers.AdminEmail,
                NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
                UserName = DefaultUsers.AdminEmail,
                NormalizedUserName = DefaultUsers.AdminEmail.ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = DefaultUsers.AdminSecurityStamp,
                ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
                PasswordHash = "AQAAAAIAAYagAAAAEF1yd41udJHyzxmeF+YfYl2moYlKqOQLUp5VCoiT6441LpRSwZ6hN0fha8PBhXqSpQ=="
            });
        }
    }
}

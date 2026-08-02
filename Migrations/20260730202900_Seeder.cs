using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pharmacy_managment.Migrations
{
    /// <inheritdoc />
    public partial class Seeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "AspNetRoles",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetRoles",
                type: "bit",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "019fb47f-ea67-7f70-a468-bc3675af7226", "019fb47f-ea67-7f70-a468-bc3746fcca98", "ApplicationRole", false, false, "Admin", "ADMIN" },
                    { "019fb47f-ea67-7f70-a468-bc381c9244d2", "019fb47f-ea67-7f70-a468-bc39c477cc73", "ApplicationRole", false, false, "Pharmacist", "PHARMACIST" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "019fb47f-ea67-7f70-a468-bc3c4efead6e", 0, "019fb47f-ea67-7f70-a468-bc3edb34cfc3", "MohammedSamirAdmin@gmail.com", true, "Mohammed", "Samir", false, null, "MOHAMMEDSAMIRADMIN@GMAIL.COM", "MOHAMMEDSAMIRADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEF1yd41udJHyzxmeF+YfYl2moYlKqOQLUp5VCoiT6441LpRSwZ6hN0fha8PBhXqSpQ==", null, false, "019fb47f-ea67-7f70-a468-bc3dc24a7256", false, "MohammedSamirAdmin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "019fb47f-ea67-7f70-a468-bc3675af7226", "019fb47f-ea67-7f70-a468-bc3c4efead6e" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019fb47f-ea67-7f70-a468-bc381c9244d2");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "019fb47f-ea67-7f70-a468-bc3675af7226", "019fb47f-ea67-7f70-a468-bc3c4efead6e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "019fb47f-ea67-7f70-a468-bc3675af7226");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019fb47f-ea67-7f70-a468-bc3c4efead6e");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetRoles");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy_managment.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicineBatch_Medicene_MediceneId",
                table: "MedicineBatch");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicineBatch_Purchases_PurchaseOrderId",
                table: "MedicineBatch");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicineBatch",
                table: "MedicineBatch");

            migrationBuilder.RenameTable(
                name: "MedicineBatch",
                newName: "medicineBatches");

            migrationBuilder.RenameIndex(
                name: "IX_MedicineBatch_PurchaseOrderId",
                table: "medicineBatches",
                newName: "IX_medicineBatches_PurchaseOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicineBatch_MediceneId",
                table: "medicineBatches",
                newName: "IX_medicineBatches_MediceneId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicineBatch_BatchName",
                table: "medicineBatches",
                newName: "IX_medicineBatches_BatchName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_medicineBatches",
                table: "medicineBatches",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_medicineBatches_Medicene_MediceneId",
                table: "medicineBatches",
                column: "MediceneId",
                principalTable: "Medicene",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_medicineBatches_Purchases_PurchaseOrderId",
                table: "medicineBatches",
                column: "PurchaseOrderId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_medicineBatches_Medicene_MediceneId",
                table: "medicineBatches");

            migrationBuilder.DropForeignKey(
                name: "FK_medicineBatches_Purchases_PurchaseOrderId",
                table: "medicineBatches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_medicineBatches",
                table: "medicineBatches");

            migrationBuilder.RenameTable(
                name: "medicineBatches",
                newName: "MedicineBatch");

            migrationBuilder.RenameIndex(
                name: "IX_medicineBatches_PurchaseOrderId",
                table: "MedicineBatch",
                newName: "IX_MedicineBatch_PurchaseOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_medicineBatches_MediceneId",
                table: "MedicineBatch",
                newName: "IX_MedicineBatch_MediceneId");

            migrationBuilder.RenameIndex(
                name: "IX_medicineBatches_BatchName",
                table: "MedicineBatch",
                newName: "IX_MedicineBatch_BatchName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicineBatch",
                table: "MedicineBatch",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineBatch_Medicene_MediceneId",
                table: "MedicineBatch",
                column: "MediceneId",
                principalTable: "Medicene",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicineBatch_Purchases_PurchaseOrderId",
                table: "MedicineBatch",
                column: "PurchaseOrderId",
                principalTable: "Purchases",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

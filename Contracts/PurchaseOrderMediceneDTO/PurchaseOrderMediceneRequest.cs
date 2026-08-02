namespace Pharmacy_managment.Contracts.PurchaseOrderMediceneDTO
{
    public record PurchaseOrderMediceneRequest
   (
        int MedicineId,
        int Quantity,
        decimal UnitPrice

   );
}

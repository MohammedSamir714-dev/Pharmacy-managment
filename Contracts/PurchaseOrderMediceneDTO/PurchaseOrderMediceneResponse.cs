namespace Pharmacy_managment.Contracts.PurchaseOrderMediceneDTO
{
    public record PurchaseOrderMediceneResponse
    (

        int MedicineId,
        string MedicineName,
        int Quantity,
        decimal UnitPrice,
        decimal TotalPrice

    );
}

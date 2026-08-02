namespace Pharmacy_managment.Contracts.InvoiceMedicenedetailsDTO
{
    public record InvoiceMedicineDetailsResponse
    (
    int MediceneId,
    string MedicineName,
    int Quantity,
    decimal UnitPrice,
    decimal TotalPrice

    );
}

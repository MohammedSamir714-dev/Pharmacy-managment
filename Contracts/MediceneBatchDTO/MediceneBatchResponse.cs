namespace Pharmacy_managment.Contracts.MediceneBatchDTO
{
    public record MediceneBatchResponse
    (
     int Id,
    string BatchName,
    DateOnly ManufactureDate,
    DateOnly ExpiryDate,
    int QuntityReceived,
    int Quntityremaining,
    int MediceneId,
    string MedicineName,
    int? PurchaseOrderId


    );
}

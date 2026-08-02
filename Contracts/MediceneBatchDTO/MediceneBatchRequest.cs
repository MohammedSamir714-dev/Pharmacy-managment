namespace Pharmacy_managment.Contracts.MediceneBatchDTO
{
    public record MediceneBatchRequest
  (
     string BatchName,
    DateOnly ManufactureDate,
    DateOnly ExpiryDate,
    int QuntityReceived,
    int MediceneId,
    int? PurchaseOrderId
  );
}

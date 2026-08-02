namespace Pharmacy_managment.Contracts.MediceneBatchDTO
{
    public record ReceiveMedicineBatchRequest(
      int MedicineId,
      string BatchName,
      DateOnly ManufactureDate,
      DateOnly ExpiryDate,
      int QuantityReceived
  );
}

namespace Pharmacy_managment.Contracts.MediceneBatchDTO
{
    public record ReceivePurchaseOrderRequest(
       List<ReceiveMedicineBatchRequest> Batches
   );
}

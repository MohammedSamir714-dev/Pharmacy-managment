using Pharmacy_managment.Contracts.PurchaseOrderMediceneDTO;

namespace Pharmacy_managment.Contracts.PurchaseOrderDTO
{
    public record PurchaseOrderResponse
    (
        int Id,
        DateTime orderDate,
        PurchaseOrderStatus PurchaseOrderStatus,
        int SupplierId,
        string SupplierName,
        decimal TotalCost,
        List<PurchaseOrderMediceneResponse> Medicines


    );
}

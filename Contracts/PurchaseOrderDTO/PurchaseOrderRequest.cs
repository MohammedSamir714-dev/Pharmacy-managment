using Pharmacy_managment.Contracts.PurchaseOrderMediceneDTO;

namespace Pharmacy_managment.Contracts.PurchaseOrderDTO
{
    public record PurchaseOrderRequest
    (
     int SupplierId,
     List<PurchaseOrderMediceneRequest> Medicines
    );
}

using Pharmacy_managment.Contracts.PurchaseOrderDTO;
using Pharmacy_managment.Contracts.PurchaseOrderMediceneDTO;

namespace Pharmacy_managment.Maping.MapingConfig
{
    public class PurChaseOrderConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PurChaseOrder, PurchaseOrderResponse>()
              .MapWith(src => new PurchaseOrderResponse
              (
                  src.Id,
                  src.orderDate,
                  src.PurchaseOrderStatus,
                  src.SupplierId,
                  src.Supplier.Name,
                  src.PurchaseOrderMedicenes.Sum(x => x.Quantity * x.UnitPrice),
                 src.PurchaseOrderMedicenes.Select(x => new PurchaseOrderMediceneResponse(
                        x.MediceneId,
                        x.Medicene.Name,
                        x.Quantity,
                        x.UnitPrice,
                        x.Quantity * x.UnitPrice
                    )).ToList()
              ));
        }
    }
}

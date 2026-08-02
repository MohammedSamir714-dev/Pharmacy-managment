using Pharmacy_managment.Contracts.PurchaseOrderMediceneDTO;

namespace Pharmacy_managment.Maping.MapingConfig
{
    public class PurchaseOrderMediceneConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PurchaseOrderMedicene, PurchaseOrderMediceneResponse>()
           .MapWith(src => new PurchaseOrderMediceneResponse
           (
               src.MediceneId,
               src.Medicene.Name,
               src.Quantity,
               src.UnitPrice,
               src.Quantity * src.UnitPrice
           ));
        }
    }
}

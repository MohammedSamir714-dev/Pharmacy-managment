using Pharmacy_managment.Contracts.MediceneBatchDTO;

namespace Pharmacy_managment.Maping.MapingConfig
{
    public class MedicineBatchMaping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<MedicineBatch, MediceneBatchResponse>()
            .MapWith(src => new MediceneBatchResponse
            (
                src.Id,
                src.BatchName,
                src.ManufactureDate,
                src.ExpiryDate,
                src.QuntityReceived,
                src.Quntityremaining,
                src.MediceneId,
                src.Medicene.Name,
                src.PurchaseOrderId
            ));
        }
    }
}

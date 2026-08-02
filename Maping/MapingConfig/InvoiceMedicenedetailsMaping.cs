using Pharmacy_managment.Contracts.InvoiceMedicenedetailsDTO;

namespace Pharmacy_managment.Maping.MapingConfig
{
    public class InvoiceMedicenedetailsMaping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<InvoiceMedicenedetails, InvoiceMedicineDetailsResponse>()
            .MapWith(src => new InvoiceMedicineDetailsResponse
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

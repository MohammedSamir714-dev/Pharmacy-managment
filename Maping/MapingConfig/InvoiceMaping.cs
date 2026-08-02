using Pharmacy_managment.Contracts.InvoiceDTO;
using Pharmacy_managment.Contracts.InvoiceMedicenedetailsDTO;

namespace Pharmacy_managment.Maping.MapingConfig
{
    public class InvoiceMaping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Invoice, InvoiceResponse>()
            .MapWith(src => new InvoiceResponse
            (
                src.Id,
                src.InvoiceDate,
                src.Paymentmethod,
                src.Customer.FullName,
                src.Pharmacist.ApplicationUser.FullName,
                src.InvoiceMedicenedetails.Sum(x => x.Quantity * x.UnitPrice),
                src.InvoiceMedicenedetails.Select(x => new InvoiceMedicineDetailsResponse(
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

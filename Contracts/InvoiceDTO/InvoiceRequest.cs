using Pharmacy_managment.Contracts.InvoiceMedicenedetailsDTO;

namespace Pharmacy_managment.Contracts.InvoiceDTO
{
    public record InvoiceRequest
    (
        int CustomerId,
        int PharmacistId,
        PaymentMethod Paymentmethod,
        List<InvoiceMedicenedetailsRequest> Medicines


    );
}

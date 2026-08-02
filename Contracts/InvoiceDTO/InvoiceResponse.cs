using Pharmacy_managment.Contracts.InvoiceMedicenedetailsDTO;

namespace Pharmacy_managment.Contracts.InvoiceDTO
{
    public record InvoiceResponse
    (
    int Id,
    DateTime InvoiceDate,
    PaymentMethod PaymentMethod,
    string CustomerName,
    string PharmacistName,
    decimal TotalAmount,
    List<InvoiceMedicineDetailsResponse> Medicines

    );
}

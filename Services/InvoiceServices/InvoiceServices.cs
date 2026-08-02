using Pharmacy_managment.Contracts.InvoiceDTO;

namespace Pharmacy_managment.Services.InvoiceServices
{
    public interface InvoiceServices
    {
        Task<Result<IEnumerable<InvoiceResponse>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<InvoiceResponse>> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Result<InvoiceResponse>> AddAsync(InvoiceRequest request, CancellationToken cancellationToken);
    }
}
using Pharmacy_managment.Contracts.SupplierDTO;

namespace Pharmacy_managment.Services.SupplierServices
{
    public interface ISupplierService
    {
        Task<Result<IEnumerable<SupplierResponse>>> GetAllAsync(CancellationToken cancellation);
        Task<Result<SupplierResponse>> GetByIdAsync(int id, CancellationToken cancellation);
        Task<Result<SupplierResponse>> AddAsync(SupplierRequest request, CancellationToken cancellation);
        Task<Result<SupplierResponse>> UpdateAsync(int id, UpdateSupplier request, CancellationToken cancellation);
        Task<Result> DeleteAsync(int id, CancellationToken cancellation);
    }
}

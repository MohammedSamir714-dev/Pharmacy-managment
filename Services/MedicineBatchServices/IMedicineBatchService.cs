using Pharmacy_managment.Contracts.MediceneBatchDTO;

namespace Pharmacy_managment.Services.MedicineBatchServices
{
    public interface IMedicineBatchService
    {
        Task<Result<IEnumerable<MediceneBatchResponse>>>GetAllAsync(CancellationToken cancellation);
        Task<Result<MediceneBatchResponse>>GetByIdAsync(int id,CancellationToken cancellation);
        Task<Result<MediceneBatchResponse>>UpdateAsync(int id,MediceneBatchRequest request,CancellationToken cancellation);
        Task<Result>DeleteAsync(int id,CancellationToken cancellation);
        Task<Result<IEnumerable<MediceneBatchResponse>>>GetExpireAsync(CancellationToken cancellation);
        Task<Result<IEnumerable<MediceneBatchResponse>>> GetExpiringSoonAsync(int days=30,CancellationToken cancellation=default);
        Task<Result<IEnumerable<MediceneBatchResponse>>>GetAvailbleBatchesAsync(int Medicineid,CancellationToken cancellation=default);
    }
}

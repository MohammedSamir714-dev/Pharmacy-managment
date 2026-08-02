using Pharmacy_managment.Contracts.PharmacistDTO;

namespace Pharmacy_managment.Services.PharmacistServices
{
    public interface IPharmacistService
    {
        Task<Result<IEnumerable<PharmacistResponse>>> GetAllAsync(CancellationToken cancellationToken);

        Task<Result<PharmacistResponse>> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<Result<PharmacistResponse>> AddAsync(PharmacistRequest request, CancellationToken cancellationToken);

        Task<Result> UpdateAsync(int id, UpdatePharmacist request, CancellationToken cancellationToken);

        Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<Result>ChangeStatusAsync(int id,UpdatePharmacistStatus status, CancellationToken cancellationToken);
    }
}

using Pharmacy_managment.Contracts.CustomerDTO;

namespace Pharmacy_managment.Services.CustomerServices
{
    public interface ICustomerService
    {
        Task<Result<IEnumerable<CustomerResponse>>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Result<CustomerResponse>> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<Result<CustomerResponse>> AddAsync(CustomerRequest request, CancellationToken cancellationToken = default);

        Task<Result> UpdateAsync(int id, UpdateCustomer request, CancellationToken cancellationToken = default);

        Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);

        Task<Result<IEnumerable<CustomerResponse>>> SearchAsync(string name, CancellationToken cancellationToken = default);
    }
}

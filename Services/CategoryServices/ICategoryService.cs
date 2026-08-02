using Pharmacy_managment.Contracts.CategoryDTO;

namespace Pharmacy_managment.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<Result<IEnumerable<CategoryResponse>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<CategoryResponse>> GetByIdAsync(int id,CancellationToken cancellationToken);
        Task<Result<CategoryResponse>> AddAsync(CategoryRequest request, CancellationToken cancellationToken);
        Task<Result> UpdateAsync(int id,UpdateCategory updateCategory, CancellationToken cancellationToken);
        Task<Result> DeleteAsync(int id,CancellationToken cancellationToken);
    }
}

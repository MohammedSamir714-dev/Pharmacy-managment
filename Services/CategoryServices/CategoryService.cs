using Pharmacy_managment.Contracts.CategoryDTO;
using Pharmacy_managment.Database;
using Pharmacy_managment.Errors;
using Pharmacy_managment.Models;

namespace Pharmacy_managment.Services.CategoryServices
{
    public class CategoryService(ApplicationDbcontext _context):ICategoryService
    {
        private readonly ApplicationDbcontext applicationDb_context = _context;

        public async Task<Result<IEnumerable<CategoryResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var Categories=await applicationDb_context.Categories
                .ProjectToType<CategoryResponse>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return Result.Success<IEnumerable<CategoryResponse>>(Categories);
        }
        public async Task<Result<CategoryResponse>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
       .Where(x => x.Id == id)
       .ProjectToType<CategoryResponse>()
       .SingleOrDefaultAsync(cancellationToken);

            if (category is null)
                return Result.Failure<CategoryResponse>(CategoryErrors.NotFound);

            return Result.Success(category);
        }
       public async Task<Result<CategoryResponse>> AddAsync(CategoryRequest request, CancellationToken cancellationToken)
        {
            var isExist = await _context.Categories
       .AnyAsync(x => x.Name == request.Name);

            if (isExist)
                return Result.Failure<CategoryResponse>(CategoryErrors.DuplicateName);

            var category = request.Adapt<Category>();

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync(cancellationToken);

            var Response = await _context.Categories
                  .AsNoTracking()
                  .Where(x => x.Id == category.Id)
                  .ProjectToType<CategoryResponse>()
                  .SingleOrDefaultAsync(cancellationToken);

            return Result.Success(Response);
        }
        public async Task<Result> UpdateAsync(int id, UpdateCategory request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category is null)
                return Result.Failure<CategoryResponse>(CategoryErrors.NotFound);

            var isExist = await _context.Categories
                .AnyAsync(x => x.Name == request.Name && x.Id != id);

            if (isExist)
                return Result.Failure<CategoryResponse>(CategoryErrors.DuplicateName);

            category.Name = request.Name;
            category.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);


            var Response = await _context.Categories
                  .AsNoTracking()
                  .Where(x => x.Id == category.Id)
                  .ProjectToType<CategoryResponse>()
                  .SingleOrDefaultAsync(cancellationToken);

            return Result.Success(Response);
        }
       public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category is null)
                return Result.Failure(CategoryErrors.NotFound);

            var hasMedicines = await _context.Medicene
                .AnyAsync(x => x.CategoryId == id);

            if (hasMedicines)
                return Result.Failure(CategoryErrors.HasMedicines);

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

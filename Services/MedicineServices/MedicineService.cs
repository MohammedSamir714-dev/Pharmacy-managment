using Pharmacy_managment.Database;
using Pharmacy_managment.Errors;

namespace Pharmacy_managment.Services.MedicineServices
{
    public class MedicineService(ApplicationDbcontext context) : IMedicineService
    {
        private readonly ApplicationDbcontext context = context;
        public async Task<Result<IEnumerable<MediceneResponse>>> GetAllAsync(CancellationToken cancellation)
        {
            var Mediciens = await context.Medicene
                .AsNoTracking()
                .ProjectToType<MediceneResponse>()
                .ToListAsync(cancellation);
            return Result.Success<IEnumerable<MediceneResponse>>(Mediciens);
        }
        public async Task<Result<MediceneResponse>> GetById(int id, CancellationToken cancellation)
        {
            var Medicine = await context.Medicene
                .Where(x => x.Id == id)
                .ProjectToType<MediceneResponse>()
                .FirstOrDefaultAsync(cancellation);

            if (Medicine is null)
                return Result.Failure<MediceneResponse>(MediceneErrors.NotFound);
            return Result.Success(Medicine);
        }
        public async Task<Result<IEnumerable<MediceneResponse>>> GetLowStockAsync(int minimumStock = 10)
        {
            var medicines = await context.Medicene
                .AsNoTracking()
                .Where(x => x.medicineBatches.Sum(b=>b.Quntityremaining) <= minimumStock)
                .ProjectToType<MediceneResponse>()
                .ToListAsync();
            return Result.Success<IEnumerable<MediceneResponse>>(medicines);
        }
        public async Task<Result<IEnumerable<MediceneResponse>>> SearchAsync(string Name, CancellationToken cancellation)
        {
            Name = Name.Trim();
            var medicines=await context.Medicene
                .AsNoTracking()
                .Where(x=>EF.Functions.Like(x.Name,$"%{Name}%"))
                .ProjectToType<MediceneResponse>()
                .ToListAsync(cancellation);
            return Result.Success<IEnumerable<MediceneResponse>>(medicines);
        }
        public async Task<Result<MediceneResponse>> AddAsync(MediceneRequest request, CancellationToken cancellation)
        {
            var CategoryExsist = await context.Categories.AnyAsync(x => x.Id == request.CategoryId);
            if (!CategoryExsist)
                return Result.Failure<MediceneResponse>(MediceneErrors.CategoryNotFound);

            var DuplicateName = await context.Medicene.AnyAsync(m => m.Name == request.Name);
            if (DuplicateName)
                return Result.Failure<MediceneResponse>(MediceneErrors.DuplicateName);

            var medicine = request.Adapt<Medicene>();
            await context.Medicene.AddAsync(medicine);
            await context.SaveChangesAsync(cancellation);
            var Response = await context.Medicene
               .AsNoTracking()
               .Where(x => x.Id == medicine.Id)
               .ProjectToType<MediceneResponse>()
               .SingleOrDefaultAsync(cancellation);

            return Result.Success(Response);

        }
        public async Task<Result> UpdateAync(int id, UpdateMedicene request, CancellationToken cancellation)
        {
            var medicine = await context.Medicene.FirstOrDefaultAsync(x => x.Id == id);
            if (medicine is null)
                return Result.Failure<MediceneResponse>(MediceneErrors.NotFound);

            var CategoryExsist = await context.Categories.AnyAsync(x => x.Id == request.CategoryId);
            if (!CategoryExsist)
                return Result.Failure<MediceneResponse>(MediceneErrors.CategoryNotFound);

            var DuplicateName = await context.Medicene.AnyAsync(m => m.Name == request.Name && m.Id != id);
            if (DuplicateName)
                return Result.Failure<MediceneResponse>(MediceneErrors.DuplicateName);
            request.Adapt(medicine);
            await context.SaveChangesAsync(cancellation);
            var Response = await context.Medicene
                .AsNoTracking()
                .Where(x => x.Id == medicine.Id)
                .ProjectToType<MediceneResponse>()
                .SingleOrDefaultAsync(cancellation);
            return Result.Success(Response);

        }
        public async Task<Result> DeleteAsync(int id, CancellationToken cancellation)
        {
            var medicine = await context.Medicene.FirstOrDefaultAsync(m => m.Id == id);
            if (medicine is null)
                return Result.Failure<MediceneResponse>(MediceneErrors.NotFound);

            if (await context.InvoicesMedicenedetails.AnyAsync(x => x.MediceneId == id))
                return Result.Failure<MediceneResponse>(MediceneErrors.HasInvoiceDetails);

            if (await context.purchaseOrderMedicenes.AnyAsync(x => x.MediceneId == id))
                return Result.Failure<MediceneResponse>(MediceneErrors.HasPurchaseOrders);

            if (await context.medicineBatches.AnyAsync(x => x.MediceneId == id)) ;
            return Result.Failure<MediceneResponse>(MediceneErrors.HasMedicineBatches);

             context.Medicene.Remove(medicine);
             await context.SaveChangesAsync(cancellation);
            return Result.Success();

        }
    }
}

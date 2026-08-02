


namespace Pharmacy_managment.Services.MedicineBatchServices
{
    public class MedicineBatchService(ApplicationDbcontext context) : IMedicineBatchService
    {
        private readonly ApplicationDbcontext context = context;
        public async Task<Result<IEnumerable<MediceneBatchResponse>>> GetAllAsync(CancellationToken cancellation)
        {
            var MedicineBatchs = await context.medicineBatches
                .AsNoTracking()
                .ProjectToType<MediceneBatchResponse>()
                .ToListAsync(cancellation);
            return Result.Success<IEnumerable<MediceneBatchResponse>>(MedicineBatchs);
        }
        public async Task<Result<MediceneBatchResponse>> GetByIdAsync(int id, CancellationToken cancellation)
        {
            var medicineBatch = await context.medicineBatches
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ProjectToType<MediceneBatchResponse>()
                .FirstOrDefaultAsync(cancellation);
            if (medicineBatch is null)
                return Result.Failure<MediceneBatchResponse>(MedicineBatchErrors.NotFound);
            return Result.Success(medicineBatch);
        }
        public async Task<Result<MediceneBatchResponse>> UpdateAsync(int id, MediceneBatchRequest request, CancellationToken cancellation)
        {
            var Batch = await context.medicineBatches.FirstOrDefaultAsync( x => x.Id == id);
            if (Batch is null)
                return Result.Failure<MediceneBatchResponse>(MedicineBatchErrors.NotFound);
            if (Batch.Quntityremaining != Batch.QuntityReceived)
                return Result.Failure<MediceneBatchResponse>(MedicineBatchErrors.AlreadyUsed);
            if (request.ManufactureDate >= request.ExpiryDate) 
            return Result.Failure<MediceneBatchResponse>(MedicineBatchErrors.InvalidManufactureDate);
            var duplicateBatch = await context.medicineBatches
                .AnyAsync(x => x.BatchName == request.BatchName && x.MediceneId == request.MediceneId
                && x.Id != id, cancellation);
            if (duplicateBatch)
                return Result.Failure<MediceneBatchResponse>(MedicineBatchErrors.DuplicateBatchName);
            request.Adapt(Batch);
            await context.SaveChangesAsync(cancellation);
            var response = await context.medicineBatches
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ProjectToType<MediceneBatchResponse>()
                .FirstOrDefaultAsync(cancellation);
            return Result.Success(response);
        }
        public async Task<Result> DeleteAsync(int id, CancellationToken cancellation)
        {
            var Batch = await context.medicineBatches.FirstOrDefaultAsync(x => x.Id == id, cancellation);
            if (Batch is null)
                return Result.Failure<MediceneBatchResponse>(MedicineBatchErrors.NotFound);
            context.medicineBatches.Remove(Batch);
            await context.SaveChangesAsync(cancellation);
            return Result.Success();
        }
        public async Task<Result<IEnumerable<MediceneBatchResponse>>> GetExpireAsync(CancellationToken cancellation)
        {
            var today=DateOnly.FromDateTime(DateTime.UtcNow);
            var batches=await context.medicineBatches
                .AsNoTracking()
                .Where(x=>x.ExpiryDate<today)
                .ProjectToType<MediceneBatchResponse>()
                .ToListAsync(cancellation);
            return Result.Success<IEnumerable<MediceneBatchResponse>>(batches);
        }
        public async Task<Result<IEnumerable<MediceneBatchResponse>>> GetExpiringSoonAsync(int days = 30, CancellationToken cancellation = default)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var endDate=today.AddDays(days);
            var batches=await context.medicineBatches
                .AsNoTracking()
                .Where (x=>x.ExpiryDate>=today&&x.ExpiryDate<=endDate)
                .OrderBy(x=>x.ExpiryDate)
                .ProjectToType<MediceneBatchResponse>()
                .ToListAsync (cancellation);
            return Result.Success<IEnumerable<MediceneBatchResponse>>(batches);
        }
        public async Task<Result<IEnumerable<MediceneBatchResponse>>> GetAvailbleBatchesAsync(int Medicineid, CancellationToken cancellation = default)
        {
            var medicineExists = await context.medicineBatches
                .AnyAsync(x => x.Id == Medicineid);
            if(!medicineExists)
                return Result.Failure<IEnumerable<MediceneBatchResponse>>(MedicineBatchErrors.NotFound);
            var batches=await context.medicineBatches .AsNoTracking()
                .Where(x=>x.MediceneId == Medicineid&&x.Quntityremaining>0)
                .ProjectToType<MediceneBatchResponse> ()
                .ToListAsync (cancellation);
            return Result.Success<IEnumerable<MediceneBatchResponse>>(batches);

        }
    }
}

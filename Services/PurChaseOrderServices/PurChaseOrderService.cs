using Pharmacy_managment.Contracts.PurchaseOrderDTO;

namespace Pharmacy_managment.Services.PurChaseOrderServices
{
    public class PurChaseOrderService(ApplicationDbcontext context):IPurChaseOrderService
    {
        public async Task<Result<IEnumerable<PurchaseOrderResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var orders = await context.Purchases
                .AsNoTracking()
                .ProjectToType<PurchaseOrderResponse>()
                .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<PurchaseOrderResponse>>(orders);
        }
        public async Task<Result<PurchaseOrderResponse>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var order = await context.Purchases
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ProjectToType<PurchaseOrderResponse>()
                .FirstOrDefaultAsync(cancellationToken);

            if (order is null)
                return Result.Failure<PurchaseOrderResponse>(PurchaseOrderErrors.NotFound);

            return Result.Success(order);
        }
        public async Task<Result<PurchaseOrderResponse>> AddAsync(PurchaseOrderRequest request, CancellationToken cancellationToken)
        {
            var supplierExists = await context.Suppliers
                .AnyAsync(x => x.Id == request.SupplierId, cancellationToken);

            if (!supplierExists)
                return Result.Failure<PurchaseOrderResponse>(SupplierErrors.NotFound);

            if (request.Medicines is null || request.Medicines.Count == 0)
                return Result.Failure<PurchaseOrderResponse>(PurchaseOrderErrors.NotFound);

            var lines = new List<PurchaseOrderMedicene>();

            foreach (var item in request.Medicines)
            {
                if (item.Quantity <= 0)
                    return Result.Failure<PurchaseOrderResponse>(PurchaseOrderErrors.NotFound);

                var medicine = await context.Medicene
                     .FirstOrDefaultAsync(x => x.Id == item.MedicineId);

                if (medicine is null)
                    return Result.Failure<PurchaseOrderResponse>(MediceneErrors.NotFound);
                medicine.Price = item.UnitPrice;
                lines.Add(new PurchaseOrderMedicene
                {
                    MediceneId = item.MedicineId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            var order = new PurChaseOrder
            {
                SupplierId = request.SupplierId,
                PurchaseOrderMedicenes = lines
            };

            await context.Purchases.AddAsync(order, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            var response = await context.Purchases
                .AsNoTracking()
                .Where(x => x.Id == order.Id)
                .ProjectToType<PurchaseOrderResponse>()
                .FirstAsync(cancellationToken);

            return Result.Success(response);
        }
        public async Task<Result> ApproveAsync(int id, CancellationToken cancellationToken)
        {
            var order = await context.Purchases
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (order is null)
                return Result.Failure(PurchaseOrderErrors.NotFound);

            if (order.PurchaseOrderStatus != PurchaseOrderStatus.Pending)
                return Result.Failure(PurchaseOrderErrors.InvalidStatus);

            order.PurchaseOrderStatus = PurchaseOrderStatus.Approved;

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<Result> ReceiveAsync(int id, ReceivePurchaseOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await context.Purchases
                .Include(x => x.PurchaseOrderMedicenes)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (order is null)
                return Result.Failure(PurchaseOrderErrors.NotFound);

            if (order.PurchaseOrderStatus != PurchaseOrderStatus.Approved)
                return Result.Failure(PurchaseOrderErrors.InvalidStatus);

            if (request.Batches is null || request.Batches.Count == 0)
                return Result.Failure(PurchaseOrderErrors.NotFound);

            var newBatches = new List<MedicineBatch>();

            foreach (var item in request.Batches)
            {
                var lineExists = order.PurchaseOrderMedicenes
                    .Any(x => x.MediceneId == item.MedicineId);

                if (!lineExists)
                    return Result.Failure(PurchaseOrderErrors.NotFound);

                if (item.ManufactureDate >= item.ExpiryDate)
                    return Result.Failure(MedicineBatchErrors.InvalidManufactureDate);

                var duplicateBatch = await context.medicineBatches
                    .AnyAsync(x => x.BatchName == item.BatchName &&
                                   x.MediceneId == item.MedicineId,
                        cancellationToken);

                if (duplicateBatch)
                    return Result.Failure(MedicineBatchErrors.DuplicateBatchName);

                newBatches.Add(new MedicineBatch
                {
                    BatchName = item.BatchName,
                    ManufactureDate = item.ManufactureDate,
                    ExpiryDate = item.ExpiryDate,
                    QuntityReceived = item.QuantityReceived,
                    Quntityremaining = item.QuantityReceived,
                    MediceneId = item.MedicineId,
                    PurchaseOrderId = order.Id
                });
            }

            order.PurchaseOrderStatus = PurchaseOrderStatus.Received;

            await context.medicineBatches.AddRangeAsync(newBatches, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<Result> CancelAsync(int id, CancellationToken cancellationToken)
        {
            var order = await context.Purchases
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (order is null)
                return Result.Failure(PurchaseOrderErrors.NotFound);

            if (order.PurchaseOrderStatus == PurchaseOrderStatus.Received)
                return Result.Failure(PurchaseOrderErrors.CannotCancelReceivedOrder);

            if (order.PurchaseOrderStatus == PurchaseOrderStatus.Cancelled)
                return Result.Failure(PurchaseOrderErrors.InvalidStatus);

            order.PurchaseOrderStatus = PurchaseOrderStatus.Cancelled;

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

using Pharmacy_managment.Contracts.InvoiceDTO;
using Pharmacy_managment.Contracts.InvoiceMedicenedetailsDTO;

namespace Pharmacy_managment.Services.InvoiceServices
{
    public class InvoiceService(ApplicationDbcontext context) : InvoiceServices
    {
        private readonly ApplicationDbcontext context = context;

        public async Task<Result<IEnumerable<InvoiceResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var invoices = await context.Invoices
                .AsNoTracking()
                .Select(i=>new InvoiceResponse(
                    i.Id,
                    i.InvoiceDate,
                    i.Paymentmethod,
                    i.Customer.FullName,
                    i.Pharmacist.ApplicationUser.FullName,
                    i.InvoiceMedicenedetails.Sum(d=>d.Quantity*d.UnitPrice),
                    i.InvoiceMedicenedetails.Select(d=> new InvoiceMedicineDetailsResponse(
                        d.MediceneId,
                        d.Medicene.Name,
                        d.Quantity,
                        d.UnitPrice,
                        d.Quantity*d.UnitPrice
                        ))
                    .ToList()

               ))
                .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<InvoiceResponse>>(invoices);
        }

        public async Task<Result<InvoiceResponse>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var invoice = await context.Invoices
                .AsNoTracking()
                .Where(x => x.Id == id).
                Select(i => new InvoiceResponse(

                                           i.Id,
                                           i.InvoiceDate,
                                           i.Paymentmethod,
                                           i.Customer.FullName,
                                           i.Pharmacist.ApplicationUser.FullName,
                                           i.InvoiceMedicenedetails.Sum(d => d.Quantity * d.UnitPrice),
                                           i.InvoiceMedicenedetails.Select(d => new InvoiceMedicineDetailsResponse(
                                               d.MediceneId,
                                               d.Medicene.Name,
                                               d.Quantity,
                                               d.UnitPrice,
                                               d.Quantity * d.UnitPrice
                                               ))
      .ToList()

                                      ))
                .FirstOrDefaultAsync(cancellationToken);

            if (invoice is null)
                return Result.Failure<InvoiceResponse>(InvoiceErrors.NotFound);

            return Result.Success(invoice);
        }

        public async Task<Result<InvoiceResponse>> AddAsync(
     InvoiceRequest request,
     CancellationToken cancellationToken)
        {
            if (request.Medicines is not { Count: > 0 })
                return Result.Failure<InvoiceResponse>(InvoiceErrors.NotFound);

            if (request.Medicines.Any(x => x.Quantity <= 0))
                return Result.Failure<InvoiceResponse>(InvoiceErrors.NotFound);

            var customerExists = await context.Customers
                .AnyAsync(x => x.Id == request.CustomerId, cancellationToken);

            if (!customerExists)
                return Result.Failure<InvoiceResponse>(CustomerErrors.NotFound);

            var pharmacistExists = await context.pharmacists
                .AnyAsync(x => x.Id == request.PharmacistId, cancellationToken);

            if (!pharmacistExists)
                return Result.Failure<InvoiceResponse>(PharmacistErrors.NotFound);

            var requestedLines = request.Medicines
                .GroupBy(x => x.MedicineId)
                .Select(g => new
                {
                    MedicineId = g.Key,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .ToList();

            var medicineIds = requestedLines
                .Select(x => x.MedicineId)
                .ToList();

            var medicines = await context.Medicene
                .Where(x => medicineIds.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id, cancellationToken);

            if (medicines.Count != medicineIds.Count)
                return Result.Failure<InvoiceResponse>(MediceneErrors.NotFound);

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var batches = await context.medicineBatches
                .Where(x => medicineIds.Contains(x.MediceneId)
                    && x.ExpiryDate > today
                    && x.Quntityremaining > 0)
                .OrderBy(x => x.ExpiryDate)
                .ToListAsync(cancellationToken);

            var batchesByMedicine = batches
                .GroupBy(x => x.MediceneId)
                .ToDictionary(x => x.Key, x => x.ToList());

            var invoiceDetails = new List<InvoiceMedicenedetails>();

            foreach (var line in requestedLines)
            {
                var medicine = medicines[line.MedicineId];

                batchesByMedicine.TryGetValue(line.MedicineId, out var medicineBatches);
                medicineBatches ??= [];

                var availableQuantity = medicineBatches.Sum(x => x.Quntityremaining);

                if (availableQuantity < line.Quantity)
                    return Result.Failure<InvoiceResponse>(
                        MediceneErrors.NotFound);

                var remainingToDeduct = line.Quantity;

                foreach (var batch in medicineBatches)
                {
                    if (remainingToDeduct == 0)
                        break;

                    var deductedQuantity = Math.Min(
                        batch.Quntityremaining,
                        remainingToDeduct);

                    batch.Quntityremaining -= deductedQuantity;
                    remainingToDeduct -= deductedQuantity;
                }

                invoiceDetails.Add(new InvoiceMedicenedetails
                {
                    MediceneId = medicine.Id,
                    Quantity = line.Quantity,
                    UnitPrice = medicine.Price
                });
            }

            var invoice = new Invoice
            {
                Paymentmethod = request.Paymentmethod,
                CustomerId = request.CustomerId,
                PharmacistId = request.PharmacistId,
                InvoiceMedicenedetails = invoiceDetails
            };

            context.Invoices.Add(invoice);

            try
            {
                await context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failure<InvoiceResponse>(
                    MediceneErrors.NotFound);
            }

            var response = await context.Invoices
                .AsNoTracking()
                .Where(i => i.Id == invoice.Id)
                .Select(i => new InvoiceResponse(
                    i.Id,
                    i.InvoiceDate,
                    i.Paymentmethod,
                    i.Customer.FullName,
                    i.Pharmacist.ApplicationUser.FullName,
                    i.InvoiceMedicenedetails.Sum(d => d.Quantity * d.UnitPrice),
                    i.InvoiceMedicenedetails.Select(d =>
                        new InvoiceMedicineDetailsResponse(
                            d.MediceneId,
                            d.Medicene.Name,
                            d.Quantity,
                            d.UnitPrice,
                            d.Quantity * d.UnitPrice
                        )).ToList()
                ))
                .SingleAsync(cancellationToken);

            return Result.Success(response);
        }
    }
}
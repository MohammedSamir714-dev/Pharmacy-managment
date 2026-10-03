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

        public async Task<Result<InvoiceResponse>> AddAsync(InvoiceRequest request, CancellationToken cancellationToken)
        {
            var customerExists = await context.Customers
                .AnyAsync(x => x.Id == request.CustomerId, cancellationToken);

            if (!customerExists)
                return Result.Failure<InvoiceResponse>(CustomerErrors.NotFound);

            var pharmacistExists = await context.pharmacists
                .AnyAsync(x => x.Id == request.PharmacistId, cancellationToken);

            if (!pharmacistExists)
                return Result.Failure<InvoiceResponse>(PharmacistErrors.NotFound);

            if (request.Medicines is null || request.Medicines.Count == 0)
                return Result.Failure<InvoiceResponse>(InvoiceErrors.NotFound);

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var invoiceDetails = new List<InvoiceMedicenedetails>();

            foreach (var item in request.Medicines)
            {
                var medicine = await context.Medicene
                    .SingleOrDefaultAsync(x => x.Id == item.MedicineId, cancellationToken);

                if (medicine is null)
                    return Result.Failure<InvoiceResponse>(MediceneErrors.NotFound);

                var batches = await context.medicineBatches
                    .Where(x => x.MediceneId == item.MedicineId &&
                                x.ExpiryDate > today &&
                                x.Quntityremaining > 0)
                    .OrderBy(x => x.ExpiryDate)
                    .ToListAsync(cancellationToken);

                var availableQuantity = batches.Sum(x => x.Quntityremaining);

                if (availableQuantity < item.Quantity)
                    return Result.Failure<InvoiceResponse>(MediceneErrors.HasMedicineBatches);

                var remainingToDeduct = item.Quantity;

                foreach (var batch in batches)
                {
                    if (remainingToDeduct <= 0)
                        break;

                    var deductFromBatch = Math.Min(batch.Quntityremaining, remainingToDeduct);
                    batch.Quntityremaining -= deductFromBatch;
                    remainingToDeduct -= deductFromBatch;
                }

                invoiceDetails.Add(new InvoiceMedicenedetails
                {
                    MediceneId = medicine.Id,
                    Quantity = item.Quantity,
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

            await context.Invoices.AddAsync(invoice, cancellationToken);

            try
            {
                await context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Result.Failure<InvoiceResponse>(MediceneErrors.NotFound);
            }

            var response = await context.Invoices
                .AsNoTracking()
                .Where(x => x.Id == invoice.Id)
                .ProjectToType<InvoiceResponse>()
                .FirstAsync(cancellationToken);

            return Result.Success(response);
        }
    }
}
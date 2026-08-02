using Pharmacy_managment.Contracts.CustomerDTO;

namespace Pharmacy_managment.Services.CustomerServices
{
    public class CustomerService(ApplicationDbcontext context, UserManager<ApplicationUser> userManager) : ICustomerService
    {
        private readonly ApplicationDbcontext context = context;

        private readonly UserManager<ApplicationUser> userManager = userManager;

        public async Task<Result<IEnumerable<CustomerResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var customers = await context.Customers
                .AsNoTracking()
                .ProjectToType<CustomerResponse>()
                .ToListAsync(cancellationToken);
            return Result.Success<IEnumerable<CustomerResponse>>(customers);
        }
        public async Task<Result<CustomerResponse>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var customer = await context.Customers
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ProjectToType<CustomerResponse>()
                .FirstOrDefaultAsync(cancellationToken);
            if (customer is null)
                return Result.Failure<CustomerResponse>(CustomerErrors.NotFound);
            return Result.Success(customer);


        }

        public async Task<Result<CustomerResponse>> AddAsync(
        CustomerRequest request,
       CancellationToken cancellationToken)
        {
            var isExist = await context.Customers
                .AnyAsync(x => x.PhoneNumber == request.PhoneNumber, cancellationToken);

            if (isExist)
                return Result.Failure<CustomerResponse>(CustomerErrors.DuplicatePhone);

            var customer = request.Adapt<Customer>();

            await context.Customers.AddAsync(customer, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            var response = customer.Adapt<CustomerResponse>();

            return Result.Success(response);
        }
        public async Task<Result> UpdateAsync(
     int id,
     UpdateCustomer request,
     CancellationToken cancellationToken)
        {
            var customer = await context.Customers
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (customer is null)
                return Result.Failure(CustomerErrors.NotFound);

            var isExist = await context.Customers
                .AnyAsync(x => x.PhoneNumber == request.PhoneNumber &&
                               x.Id != id,
                    cancellationToken);

            if (isExist)
                return Result.Failure(CustomerErrors.DuplicatePhone);

            request.Adapt(customer);

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<Result> DeleteAsync(
     int id,
     CancellationToken cancellationToken)
        {
            var customer = await context.Customers
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (customer is null)
                return Result.Failure(CustomerErrors.NotFound);

            var hasInvoices = await context.Invoices
                .AnyAsync(x => x.CustomerId == id, cancellationToken);

            if (hasInvoices)
                return Result.Failure(CustomerErrors.HasInvoices);

            context.Customers.Remove(customer);

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<Result<IEnumerable<CustomerResponse>>> SearchAsync(
    string name,
    CancellationToken cancellationToken)
        {
            var customers = await context.Customers
                .AsNoTracking()
                .Where(x => x.FullName.Contains(name))
                .ProjectToType<CustomerResponse>()
                .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<CustomerResponse>>(customers);
        }
    }
}

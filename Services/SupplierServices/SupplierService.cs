using System.Threading;
using Microsoft.EntityFrameworkCore;
using Pharmacy_managment.Contracts.CategoryDTO;
using Pharmacy_managment.Contracts.SupplierDTO;
using Pharmacy_managment.Database;
using Pharmacy_managment.Errors;
using Pharmacy_managment.Models;

namespace Pharmacy_managment.Services.SupplierServices
{
    public class SupplierService(ApplicationDbcontext context) : ISupplierService
    {
        private readonly ApplicationDbcontext context = context;
        public async Task<Result<IEnumerable<SupplierResponse>>> GetAllAsync(CancellationToken cancellation)
        {
            var Supplier = await context.Suppliers
                .AsNoTracking()
                .ProjectToType<SupplierResponse>()
                .ToListAsync(cancellation);

            return Result.Success<IEnumerable<SupplierResponse>>(Supplier);
        }
        public async Task<Result<SupplierResponse>> GetByIdAsync(int id,CancellationToken cancellation)
        {
            var response = await context.Suppliers
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ProjectToType<SupplierResponse>()
                .SingleOrDefaultAsync(cancellation);

            if (response is null)
                return Result.Failure<SupplierResponse>(SupplierErrors.NotFound);

            return Result.Success(response);
        }
        public async Task<Result<SupplierResponse>> AddAsync(SupplierRequest request,CancellationToken cancellation)
        {
          
           var duplicate= await context.Suppliers.AsNoTracking()
                .Where(x=>x.Email==request.Email||x.Phone==request.Phone)
                .Select(x=>new {EmailMatch=x.Email==request.Email})
                .FirstOrDefaultAsync(cancellation);
            if (duplicate is not null)
                return Result.Failure<SupplierResponse>(
                    duplicate.EmailMatch
                    ? SupplierErrors.DuplicateEmail : SupplierErrors.DuplicatePhone
                    );

            var supplier = request.Adapt<Supplier>();
            await context.Suppliers.AddAsync(supplier,cancellation);
            await context.SaveChangesAsync(cancellation);

            var Response = new SupplierResponse(
                supplier.Id,
                supplier.Name,
                supplier.Email,
                supplier.Phone
                );
                 
            return Result.Success(Response);


        }
        public async Task<Result<SupplierResponse>> UpdateAsync(int id, UpdateSupplier request, CancellationToken cancellation)
        {
            var supplier = await context.Suppliers
           .FirstOrDefaultAsync(x => x.Id == id);

            if (supplier is null)
                return Result.Failure<SupplierResponse>(SupplierErrors.NotFound);

            var emailExists = await context.Suppliers
           .AnyAsync(x => x.Email == request.Email && x.Id != id);

            if (emailExists)
                return Result.Failure<SupplierResponse>(SupplierErrors.DuplicateEmail);

            var PhoneExists = await context.Suppliers
          .AnyAsync(x => x.Phone == request.Phone&&x.Id!=id);

            if (PhoneExists)
                return Result.Failure<SupplierResponse>(SupplierErrors.DuplicatePhone);
           request.Adapt(supplier);
            await context.SaveChangesAsync(cancellation);

            var Response = await context.Suppliers
                  .AsNoTracking()
                  .Where(x => x.Id == supplier.Id)
                  .ProjectToType<SupplierResponse>()
                  .SingleOrDefaultAsync(cancellation);

            return Result.Success(Response);

        }
      public async Task<Result> DeleteAsync(int id, CancellationToken cancellation)
        {
            var supplier = await context.Suppliers
           .FirstOrDefaultAsync(x => x.Id == id);
            if (supplier is null)
                return Result.Failure<SupplierResponse>(SupplierErrors.NotFound);
            var hasPurchaseOrders = await context.Purchases
          .AnyAsync(x => x.SupplierId == id);

            if (hasPurchaseOrders)
                return Result.Failure(SupplierErrors.HasPurchaseOrders);

            context.Suppliers.Remove(supplier);
            await context.SaveChangesAsync();

            return Result.Success();
        }

    }
}

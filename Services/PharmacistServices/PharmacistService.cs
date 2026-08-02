using Pharmacy_managment.Abstractions.Consts;
using Pharmacy_managment.Contracts.PharmacistDTO;

namespace Pharmacy_managment.Services.PharmacistServices
{
    public class PharmacistService(ApplicationDbcontext context,UserManager<ApplicationUser> userManager):IPharmacistService
    {
        private readonly ApplicationDbcontext context = context;
        private readonly UserManager<ApplicationUser> userManager = userManager;

        public async Task<Result<IEnumerable<PharmacistResponse>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var pharmacists = await context.pharmacists
                .AsNoTracking()
                .ProjectToType<PharmacistResponse>()
                .ToListAsync(cancellationToken);

            return Result.Success<IEnumerable<PharmacistResponse>>(pharmacists);
        }
        public async Task<Result<PharmacistResponse>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var pharmacist = await context.pharmacists
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ProjectToType<PharmacistResponse>()
                .FirstOrDefaultAsync(cancellationToken);

            if (pharmacist is null)
                return Result.Failure<PharmacistResponse>(PharmacistErrors.NotFound);

            return Result.Success(pharmacist);
        }
        public async Task<Result<PharmacistResponse>> AddAsync(PharmacistRequest request, CancellationToken cancellationToken)
        {
            if (await userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken))
                return Result.Failure<PharmacistResponse>(UserErrors.DuplicatedEmail);

            if (await context.pharmacists.AnyAsync(x => x.Licensenumber == request.Licensenumber, cancellationToken))
                return Result.Failure<PharmacistResponse>(PharmacistErrors.DuplicateLicenseNumber);

            var user = request.Adapt<ApplicationUser>();
            user.UserName = request.Email;
            user.EmailConfirmed = true;

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var error = result.Errors.First();

                return Result.Failure<PharmacistResponse>(new Error(
                    error.Code,
                    error.Description,
                    StatusCodes.Status400BadRequest));
            }
            var roleResult = await userManager.AddToRoleAsync(user, DefaultRoles.Pharmacist);

            if (!roleResult.Succeeded)
            {
                await userManager.DeleteAsync(user);

                var error = roleResult.Errors.First();

                return Result.Failure<PharmacistResponse>(new Error(
                    error.Code,
                    error.Description,
                    StatusCodes.Status400BadRequest));
            }
            var pharmacist = request.Adapt<Pharmacist>();
            pharmacist.Status = PharmacistStatus.OffDuty;
            pharmacist.ApplicationUserId = user.Id;

            await context.pharmacists.AddAsync(pharmacist, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            var response = await context.pharmacists
     .AsNoTracking()
     .Where(x => x.Id == pharmacist.Id)
     .ProjectToType<PharmacistResponse>()
     .FirstAsync(cancellationToken);

            return Result.Success(response); ;
        }
        public async Task<Result> UpdateAsync(int id,UpdatePharmacist request,CancellationToken cancellationToken)
        {
            var pharmacist = await context.pharmacists
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (pharmacist is null)
                return Result.Failure(PharmacistErrors.NotFound);

            var user = await context.Users
                .SingleOrDefaultAsync(x => x.Id == pharmacist.ApplicationUserId, cancellationToken);

            if (user is null)
                return Result.Failure(UserErrors.InvalidCode);

            var duplicatedEmail = await context.Users
                .AnyAsync(x => x.Email == request.Email &&
                               x.Id != user.Id,
                    cancellationToken);

            if (duplicatedEmail)
                return Result.Failure(UserErrors.DuplicatedEmail);

            var duplicatedLicense = await context.pharmacists
                .AnyAsync(x => x.Licensenumber == request.Licensenumber &&
                               x.Id != id,
                    cancellationToken);

            if (duplicatedLicense)
                return Result.Failure(PharmacistErrors.DuplicateLicenseNumber);

            request.Adapt(pharmacist);
            request.Adapt(user);
            user.UserName = request.Email;

            await context.SaveChangesAsync(cancellationToken);
            var response = await context.pharmacists
          .AsNoTracking()
          .Where(x => x.Id == pharmacist.Id)
          .ProjectToType<PharmacistResponse>()
           .FirstAsync(cancellationToken);

            return Result.Success(response);

        }
        public async Task<Result> DeleteAsync(
    int id,
    CancellationToken cancellationToken)
        {
            var pharmacist = await context.pharmacists
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (pharmacist is null)
                return Result.Failure(PharmacistErrors.NotFound);

            var hasInvoices = await context.Invoices
                .AnyAsync(x => x.PharmacistId == id, cancellationToken);

            if (hasInvoices)
                return Result.Failure(PharmacistErrors.HasInvoices);

            var user = await context.Users
                .SingleOrDefaultAsync(x => x.Id == pharmacist.ApplicationUserId, cancellationToken);

            context.pharmacists.Remove(pharmacist);

            if (user is not null)
                context.Users.Remove(user);

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        public async Task<Result> ChangeStatusAsync(int id,UpdatePharmacistStatus request,CancellationToken cancellationToken)
        {
            var pharmacist = await context.pharmacists
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

            if (pharmacist is null)
                return Result.Failure(PharmacistErrors.NotFound);

            if (pharmacist.Status == request.Status)
                return Result.Failure(PharmacistErrors.InvalidStatus);

            pharmacist.Status = request.Status;

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

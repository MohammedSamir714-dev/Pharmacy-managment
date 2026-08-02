using Pharmacy_managment.Abstractions;
using Pharmacy_managment.Contracts.Users;

namespace Pharmacy_managment.Services.AccountUserServices;

public interface IUserService
{
    Task<Result<UserProfileResponse>> GetProfileAsync(string userId);
    Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request);
    Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request);
}
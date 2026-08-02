namespace Pharmacy_managment.Contracts.Users;

public record ChangePasswordRequest(
    string CurrentPassword,
    string NewPassword
);
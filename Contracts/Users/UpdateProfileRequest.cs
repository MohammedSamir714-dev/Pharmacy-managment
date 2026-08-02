namespace Pharmacy_managment.Contracts.Users;

public record UpdateProfileRequest(
    string FirstName,
    string LastName
);
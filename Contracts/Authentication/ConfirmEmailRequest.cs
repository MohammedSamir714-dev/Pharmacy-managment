namespace Pharmacy_managment.Contracts.Authentication;

public record ConfirmEmailRequest(
    string Email,
    string Code
);
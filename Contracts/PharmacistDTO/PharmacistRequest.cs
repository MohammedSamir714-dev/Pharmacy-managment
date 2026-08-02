namespace Pharmacy_managment.Contracts.PharmacistDTO
{
    public record PharmacistRequest
    (
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PhoneNumber,
    string Licensenumber,
    decimal Salary,
    DateOnly HireDate

    );
}

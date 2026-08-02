namespace Pharmacy_managment.Contracts.PharmacistDTO
{
    public record UpdatePharmacist
    (
    string FirstName,
    string LastName,
    string Email,
    string Licensenumber,
    decimal Salary,
    DateOnly HireDate,
    string PhoneNumber

    );
}

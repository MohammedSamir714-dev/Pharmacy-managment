namespace Pharmacy_managment.Contracts.PharmacistDTO
{
    public record PharmacistResponse
    (
     int Id,
    string FullName,
    string Email,
    string PhoneNumber,
    string Licensenumber,
    decimal Salary,
    DateOnly HireDate,
    string Status

    );
}

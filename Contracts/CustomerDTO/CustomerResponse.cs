namespace Pharmacy_managment.Contracts.CustomerDTO
{
    public record CustomerResponse
    (
       int Id,
       string FullName,
       string PhoneNumber,
       string Address
    );
}

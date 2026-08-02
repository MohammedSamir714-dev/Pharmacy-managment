namespace Pharmacy_managment.Contracts.MediceneDTO
{
    public record MediceneRequest
    (
        string Name,
        decimal Price,
        int CategoryId
    );
}

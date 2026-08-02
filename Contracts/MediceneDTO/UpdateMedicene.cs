namespace Pharmacy_managment.Contracts.MediceneDTO
{
    public record UpdateMedicene
    (
        string Name,
        decimal Price,
        int CategoryId
    );
}

using Pharmacy_managment.Contracts.MediceneBatchDTO;

namespace Pharmacy_managment.Contracts.MediceneDTO
{
    public record MediceneResponse
    (
        int Id,
        string Name,
        decimal Price,
        int CategoryId,
        string CategoryName,
        int TotalQuantityInStock
    );
}

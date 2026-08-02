namespace Pharmacy_managment.Contracts.CategoryDTO
{
    public record CategoryResponse
    (
        int Id,
        string Name,
        string Description,
        int MedicinesCount
    );
}

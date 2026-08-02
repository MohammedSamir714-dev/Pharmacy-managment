
namespace Pharmacy_managment.Errors
{
    public static class CategoryErrors
    {
        public static readonly Error NotFound =
        new(
            "Category.NotFound",
            "Category was not found.",
            StatusCodes.Status404NotFound);

        public static readonly Error DuplicateName =
            new(
                "Category.DuplicateName",
                "A category with the same name already exists.",
                StatusCodes.Status409Conflict);

        public static readonly Error HasMedicines =
            new(
                "Category.HasMedicines",
                "The category cannot be deleted because it contains medicines.",
                StatusCodes.Status409Conflict);
    }
}

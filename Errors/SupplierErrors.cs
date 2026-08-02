namespace Pharmacy_managment.Errors
{
    public static class SupplierErrors
    {
        public static readonly Error NotFound = new("Supplier.NotFound", "Supplier not found", StatusCodes.Status404NotFound);

        public static readonly Error DuplicateEmail =
            new(
                "Supplier.DuplicateEmail",
                "A supplier with the same email already exists.",
                StatusCodes.Status409Conflict);

        public static readonly Error DuplicateName =
            new(
                "Supplier.DuplicateName",
                "A supplier with the same Name already exists.",
                StatusCodes.Status409Conflict);

        public static readonly Error DuplicatePhone =
            new(
                "Supplier.DuplicatePhone",
                "A supplier with the same Phone already exists.",
                StatusCodes.Status409Conflict);

        public static readonly Error HasPurchaseOrders =
            new(
                "Supplier.HasPurchaseOrders",
                "The supplier cannot be deleted because it has purchase orders.",
                StatusCodes.Status409Conflict);
    }
}

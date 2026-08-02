namespace Pharmacy_managment.Errors
{
    public static class MedicineBatchErrors
    {
        public static readonly Error NotFound =
       new(
           "MedicineBatch.NotFound",
           "Medicine batch was not found.",
           StatusCodes.Status404NotFound);

        public static readonly Error InvalidExpiryDate =
            new(
                "MedicineBatch.InvalidExpiryDate",
                "Expiry date must be later than manufacture date.",
                StatusCodes.Status400BadRequest);

        public static readonly Error InvalidManufactureDate =
            new(
                "MedicineBatch.InvalidManufactureDate",
                "Manufacture date cannot be in the future.",
                StatusCodes.Status400BadRequest);

        public static readonly Error InvalidQuantity =
            new(
                "MedicineBatch.InvalidQuantity",
                "Quantity received must be greater than zero.",
                StatusCodes.Status400BadRequest);

        public static readonly Error MedicineNotFound =
            new(
                "MedicineBatch.MedicineNotFound",
                "Medicine was not found.",
                StatusCodes.Status404NotFound);

        public static readonly Error DuplicateBatchName =
            new(
                "MedicineBatch.DuplicateBatchName",
                "A batch with the same name already exists.",
                StatusCodes.Status409Conflict);
        public static readonly Error AlreadyUsed =
        new(
            "MedicineBatch.AlreadyUsed",
            "The Batch is AlreadyUsed.",
            StatusCodes.Status304NotModified);
    }
}

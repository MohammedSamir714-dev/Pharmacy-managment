namespace Pharmacy_managment.Errors
{
    public static class InvoiceMedicineDetailsErrors
    {
        public static readonly Error MedicineNotFound =
       new(
           "InvoiceMedicineDetails.MedicineNotFound",
           "Medicine was not found.",
           StatusCodes.Status404NotFound);

        public static readonly Error InvalidQuantity =
            new(
                "InvoiceMedicineDetails.InvalidQuantity",
                "Quantity must be greater than zero.",
                StatusCodes.Status400BadRequest);

        public static readonly Error InsufficientStock =
            new(
                "InvoiceMedicineDetails.InsufficientStock",
                "Insufficient stock for the requested medicine.",
                StatusCodes.Status409Conflict);

        public static readonly Error DuplicateMedicine =
            new(
                "InvoiceMedicineDetails.DuplicateMedicine",
                "Duplicate medicines are not allowed.",
                StatusCodes.Status409Conflict);
    }
}

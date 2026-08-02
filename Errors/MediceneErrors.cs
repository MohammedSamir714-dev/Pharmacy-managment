namespace Pharmacy_managment.Errors
{
    public static class MediceneErrors
    {
        public static readonly Error NotFound =
       new(
           "Medicine.NotFound",
           "Medicine was not found.",
           StatusCodes.Status404NotFound);

        public static readonly Error DuplicateName =
            new(
                "Medicine.DuplicateName",
                "A medicine with the same name already exists.",
                StatusCodes.Status409Conflict);

        public static readonly Error CategoryNotFound =
            new(
                "Medicine.CategoryNotFound",
                "Category was not found.",
                StatusCodes.Status404NotFound);

        public static readonly Error HasInvoiceDetails =
            new(
                "Medicine.HasInvoiceDetails",
                "The medicine cannot be deleted because it is used in invoices.",
                StatusCodes.Status409Conflict);

        public static readonly Error HasPurchaseOrders =
            new(
                "Medicine.HasPurchaseOrders",
                "The medicine cannot be deleted because it is linked to purchase orders.",
                StatusCodes.Status409Conflict);

        public static readonly Error HasMedicineBatches =
            new(
                "Medicine.HasMedicineBatches",
                "The medicine cannot be deleted because it has inventory batches.",
                StatusCodes.Status409Conflict);
    }
}


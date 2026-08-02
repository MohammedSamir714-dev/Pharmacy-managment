namespace Pharmacy_managment.Errors
{
    public static class InvoiceErrors
    {
        public static readonly Error NotFound =
        new(
            "Invoice.NotFound",
            "Invoice was not found.",
            StatusCodes.Status404NotFound);

        public static readonly Error CustomerNotFound =
            new(
                "Invoice.CustomerNotFound",
                "Customer was not found.",
                StatusCodes.Status404NotFound);

        public static readonly Error PharmacistNotFound =
            new(
                "Invoice.PharmacistNotFound",
                "Pharmacist was not found.",
                StatusCodes.Status404NotFound);

        public static readonly Error InvalidPaymentMethod =
            new(
                "Invoice.InvalidPaymentMethod",
                "Invalid payment method.",
                StatusCodes.Status400BadRequest);
    }
}

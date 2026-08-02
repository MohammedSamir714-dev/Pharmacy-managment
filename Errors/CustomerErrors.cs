namespace Pharmacy_managment.Errors
{
    public static class CustomerErrors
    {
        public static readonly Error NotFound =
        new(
            "Customer.NotFound",
            "Customer was not found.",
            StatusCodes.Status404NotFound);

        public static readonly Error DuplicatePhone =
            new(
                "Customer.DuplicatePhone",
                "A customer with the same Phone already exists.",
                StatusCodes.Status409Conflict);

        public static readonly Error HasInvoices =
            new(
                "Customer.HasInvoices",
                "The customer cannot be deleted because there are invoices associated with them.",
                StatusCodes.Status409Conflict);
    }
}

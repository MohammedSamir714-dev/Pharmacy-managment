namespace Pharmacy_managment.Errors
{
    public static class PharmacistErrors
    {
        public static readonly Error NotFound =
      new(
          "Pharmacist.NotFound",
          "Pharmacist was not found.",
          StatusCodes.Status404NotFound);

        public static readonly Error DuplicateLicenseNumber =
            new(
                "Pharmacist.DuplicateLicenseNumber",
                "A pharmacist with the same license number already exists.",
                StatusCodes.Status409Conflict);

        public static readonly Error InvalidStatus =
            new(
                "Pharmacist.InvalidStatus",
                "Invalid pharmacist status.",
                StatusCodes.Status400BadRequest);

        public static readonly Error HasInvoices =
            new(
                "Pharmacist.HasInvoices",
                "The pharmacist cannot be deleted because there are invoices associated with them.",
                StatusCodes.Status409Conflict);
    }
}

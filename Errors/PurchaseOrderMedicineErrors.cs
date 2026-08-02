namespace Pharmacy_managment.Errors
{
    public static class PurchaseOrderMedicineErrors
    {
        public static readonly Error MedicineNotFound =
      new(
          "PurchaseOrderMedicine.MedicineNotFound",
          "Medicine was not found.",
          StatusCodes.Status404NotFound);

        public static readonly Error InvalidQuantity =
            new(
                "PurchaseOrderMedicine.InvalidQuantity",
                "Quantity must be greater than zero.",
                StatusCodes.Status400BadRequest);

        public static readonly Error InvalidUnitPrice =
            new(
                "PurchaseOrderMedicine.InvalidUnitPrice",
                "Unit price must be greater than zero.",
                StatusCodes.Status400BadRequest);

        public static readonly Error DuplicateMedicine =
            new(
                "PurchaseOrderMedicine.DuplicateMedicine",
                "Duplicate medicines are not allowed.",
                StatusCodes.Status409Conflict);
    }
}

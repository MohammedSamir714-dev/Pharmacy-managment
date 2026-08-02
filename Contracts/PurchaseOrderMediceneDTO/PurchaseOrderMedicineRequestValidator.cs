namespace Pharmacy_managment.Contracts.PurchaseOrderMediceneDTO
{
    public class PurchaseOrderMedicineRequestValidator:AbstractValidator<PurchaseOrderMediceneRequest>
    {
        public PurchaseOrderMedicineRequestValidator()
        {
            RuleFor(x => x.MedicineId)
                .GreaterThan(0)
                .WithMessage("A valid medicine is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.")
                .LessThanOrEqualTo(100000)
                .WithMessage("Quantity is too large.");

            RuleFor(x => x.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Unit price must be greater than zero.")
                .LessThanOrEqualTo(1000000)
                .WithMessage("Unit price is too large.");
        }
    }
}

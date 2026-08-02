using Pharmacy_managment.Contracts.PurchaseOrderMediceneDTO;

namespace Pharmacy_managment.Contracts.PurchaseOrderDTO
{
    public class PurchaseOrderRequestValidator : AbstractValidator<PurchaseOrderRequest>
    {
        public PurchaseOrderRequestValidator()
        {
            RuleFor(x => x.SupplierId)
            .GreaterThan(0)
            .WithMessage("Supplier Id is required.");

            RuleFor(x => x.Medicines)
                .NotNull()
                .WithMessage("Medicines are required.")
                .NotEmpty()
                .WithMessage("At least one medicine is required.");

            RuleForEach(x => x.Medicines)
                .SetValidator(new PurchaseOrderMedicineRequestValidator());

            RuleFor(x => x.Medicines)
                .Must(x => x.Select(m => m.MedicineId).Distinct().Count() == x.Count)
                .WithMessage("Duplicate medicines are not allowed.");
        }
    }
}

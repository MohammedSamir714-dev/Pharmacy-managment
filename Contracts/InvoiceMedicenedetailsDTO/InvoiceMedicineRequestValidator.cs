namespace Pharmacy_managment.Contracts.InvoiceMedicenedetailsDTO
{
    public class InvoiceMedicineRequestValidator : AbstractValidator<InvoiceMedicenedetailsRequest>
    {
        public InvoiceMedicineRequestValidator()
        {
            RuleFor(x => x.MedicineId)
            .GreaterThan(0)
            .WithMessage("A valid medicine is required.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero.");
        }
    }
}

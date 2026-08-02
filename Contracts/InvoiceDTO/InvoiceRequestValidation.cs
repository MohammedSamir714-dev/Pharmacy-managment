using FluentValidation;
using Pharmacy_managment.Contracts.InvoiceMedicenedetailsDTO;

namespace Pharmacy_managment.Contracts.InvoiceDTO
{
    public class InvoiceRequestValidation : AbstractValidator<InvoiceRequest>
    {
        public InvoiceRequestValidation()
        {
            RuleFor(x => x.CustomerId)
            .GreaterThan(0)
            .WithMessage("A valid customer is required.");

            RuleFor(x => x.PharmacistId)
            .GreaterThan(0)
            .WithMessage("A valid pharmacist is required.");

            RuleFor(x => x.Paymentmethod)
            .IsInEnum()
            .WithMessage("Invalid payment method.");

            RuleFor(x => x.Medicines)
           .NotNull()
           .WithMessage("Medicines are required.")
           .NotEmpty()
           .WithMessage("Invoice must contain at least one medicine.");

            RuleForEach(x => x.Medicines)
            .SetValidator(new InvoiceMedicineRequestValidator());

            RuleFor(x => x.Medicines)
                .Must(medicines => medicines
                    .Select(m => m.MedicineId)
                    .Distinct()
                    .Count() == medicines.Count)
                .WithMessage("Duplicate medicines are not allowed in the same invoice.");
        }
    }
}

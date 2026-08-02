namespace Pharmacy_managment.Contracts.MediceneBatchDTO
{
    public class MedicineBatchRequestValidator : AbstractValidator<MediceneBatchRequest>
    {
        public MedicineBatchRequestValidator()
        {
            RuleFor(x => x.BatchName)
           .NotEmpty().WithMessage("Batch name is required.")
           .MaximumLength(100).WithMessage("Batch name must not exceed 100 characters.");

            RuleFor(x => x.ManufactureDate)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Manufacture date cannot be in the future.");

            RuleFor(x => x.ExpiryDate)
                .GreaterThan(x => x.ManufactureDate)
                .WithMessage("Expiry date must be later than manufacture date.");

            RuleFor(x => x.QuntityReceived)
                .GreaterThan(0)
                .WithMessage("Quantity received must be greater than zero.");

            RuleFor(x => x.MediceneId)
                .GreaterThan(0)
                .WithMessage("Medicine Id is required.");
        }
    }
}

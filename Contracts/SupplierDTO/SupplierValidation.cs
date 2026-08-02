namespace Pharmacy_managment.Contracts.SupplierDTO
{
    public class SupplierValidation : AbstractValidator<SupplierRequest>
    {
        public SupplierValidation()
        {
            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Supplier name is required.")
           .MinimumLength(3).WithMessage("Supplier name must be at least 3 characters.")
           .MaximumLength(100).WithMessage("Supplier name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
          .NotEmpty().WithMessage("Email is required.")
          .EmailAddress().WithMessage("Invalid email address.")
          .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.");

            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^01[0125][0-9]{8}$")
             .WithMessage("Invalid Egyptian mobile number.");
        }
    }
}

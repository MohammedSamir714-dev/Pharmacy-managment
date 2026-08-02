namespace Pharmacy_managment.Contracts.PharmacistDTO
{
    public class PharmacistValidation : AbstractValidator<PharmacistRequest>
    {
        public PharmacistValidation()
        {
            RuleFor(x => x.FirstName)
           .NotEmpty().WithMessage("First name is required.")
           .MinimumLength(2).WithMessage("First name must be at least 2 characters.")
           .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MinimumLength(2).WithMessage("Last name must be at least 2 characters.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email is required.")
           .EmailAddress().WithMessage("Invalid email address.")
           .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.");

            RuleFor(x => x.Password)
           .NotEmpty().WithMessage("Password is required.")
           .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
           .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
           .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
           .Matches(@"\d").WithMessage("Password must contain at least one number.");

            RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.");

            RuleFor(x => x.Licensenumber)
          .NotEmpty().WithMessage("License number is required.")
          .MaximumLength(50).WithMessage("License number must not exceed 50 characters.");

            RuleFor(x => x.Salary)
            .GreaterThan(0).WithMessage("Salary must be greater than zero.")
            .LessThanOrEqualTo(1000000).WithMessage("Salary is too large.");

            RuleFor(x => x.HireDate)
          .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
          .WithMessage("Hire date cannot be in the future.");


        }
    }
}

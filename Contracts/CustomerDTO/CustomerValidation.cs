namespace Pharmacy_managment.Contracts.CustomerDTO
{
    public class CustomerValidation : AbstractValidator<CustomerRequest>
    {
        public CustomerValidation()
        {
            RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MinimumLength(2).WithMessage("Full name must be at least 2 characters.")
            .MaximumLength(50).WithMessage("Full name cannot exceed 50 characters.");




            RuleFor(x => x.Address)
           .NotEmpty().WithMessage("Address is required.")
           .MinimumLength(5).WithMessage("Address must be at least 5 characters.")
           .MaximumLength(255).WithMessage("Address cannot exceed 255 characters.");

            RuleFor(x => x.PhoneNumber)
           .NotEmpty().WithMessage("Phone number is required.")
           .Matches(@"^01[0125][0-9]{8}$")
           .WithMessage("Invalid Egyptian mobile number.");
        }
    }
}

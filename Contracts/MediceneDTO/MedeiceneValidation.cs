namespace Pharmacy_managment.Contracts.MediceneDTO
{
    public class MedeiceneValidation : AbstractValidator<MediceneRequest>
    {
        public MedeiceneValidation()
        {
            RuleFor(x => x.Name)
          .NotEmpty().WithMessage("Medicine name is required.")
          .MinimumLength(2).WithMessage("Medicine name must be at least 2 characters.")
          .MaximumLength(150).WithMessage("Medicine name cannot exceed 150 characters.");

            RuleFor(x => x.Price)
           .GreaterThan(0).WithMessage("Price must be greater than zero.")
           .LessThan(1000000).WithMessage("Price is too large.");

           
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("A valid category is required.");
        }
    }
}

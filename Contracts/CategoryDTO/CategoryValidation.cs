namespace Pharmacy_managment.Contracts.CategoryDTO
{
    public class CategoryValidation : AbstractValidator<CategoryRequest>
    {
        public CategoryValidation()
        {
            RuleFor(x => x.Name)
          .NotEmpty().WithMessage("Category name is required.")
          .MinimumLength(3).WithMessage("Category name must be at least 3 characters.")
          .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters.");


            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(10).WithMessage("Description must be at least 10 characters.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}

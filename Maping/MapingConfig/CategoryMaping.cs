using Pharmacy_managment.Contracts.CategoryDTO;

namespace Pharmacy_managment.Maping.MapingConfig
{
    public class CategoryMaping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Category, CategoryResponse>()
                .MapWith(c => new CategoryResponse(
                    c.Id,
                    c.Name,
                    c.Description,
                    c.medicenes.Count
                    ));
        }
    }
}

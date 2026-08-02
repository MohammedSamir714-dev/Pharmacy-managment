
using Pharmacy_managment.Models;

namespace Pharmacy_managment.Maping.MapingConfig
{
    public class MediceneMaping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Medicene, MediceneResponse>()
            .MapWith(src => new MediceneResponse
            (
                src.Id,
                src.Name,
                src.Price,
                src.CategoryId,
                src.Category.Name,
                src.medicineBatches.Sum(b => b.Quntityremaining)
            ));
            //config.NewConfig<Medicene, MediceneResponse>()
            //    .Map(dest => dest.CategoryName, src => src.Category.Name)
            //    .Map(dest => dest.TotalQuantityInStock, src => src.TotalQuntityInStock);

        }
    }
}

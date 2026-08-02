using Pharmacy_managment.Contracts.SupplierDTO;

namespace Pharmacy_managment.Maping.MapingConfig
{
    public class SupplierMaping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Supplier, SupplierResponse>()
                .MapWith(c => new SupplierResponse(
                   c.Id,
                   c.Name,
                   c.Email,
                   c.Phone
                    ));
        }
    }
}

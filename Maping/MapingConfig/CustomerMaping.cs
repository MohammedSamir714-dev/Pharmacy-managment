using Pharmacy_managment.Contracts.CustomerDTO;

namespace Pharmacy_managment.Maping.MapingConfig
{
    public class CustomerMaping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Customer, CustomerResponse>()
            .MapWith(src => new CustomerResponse
            (
                src.Id,
               src.FullName,
               src.PhoneNumber,
               src.Address
            ));
        }
    }
}

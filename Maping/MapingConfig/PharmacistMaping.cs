using Pharmacy_managment.Contracts.PharmacistDTO;

namespace Pharmacy_managment.Maping.MapingConfig
{
    public class PharmacistMaping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Pharmacist, PharmacistResponse>()
             .MapWith(src => new PharmacistResponse
             (
                 src.Id,
                 src.ApplicationUser.FullName,
                 src.ApplicationUser.Email!,
                 src.ApplicationUser.PhoneNumber!,
                 src.Licensenumber,
                 src.Salary,
                 src.HireDate,
                 src.Status.ToString()
             ));
        }
    }
}

namespace Pharmacy_managment.Models
{
    public class ApplicationRole: IdentityRole
    {
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
    }
}

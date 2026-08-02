

namespace Pharmacy_managment.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}".Trim();
        public List<RefreshToken> RefreshTokens { get; set; } = [];
        public Pharmacist Pharmacist { get; set; }
    }
}

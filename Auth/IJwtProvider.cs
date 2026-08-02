using Pharmacy_managment.Models;

namespace Pharmacy_managment.Auth
{
    public interface IJwtProvider
    {
        public (string token, int expirIn) GenerateToken(ApplicationUser user, IEnumerable<string> roles);
        string? ValidateToken(string token);
    }
}

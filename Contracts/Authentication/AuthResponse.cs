namespace Pharmacy_managment.Contract.Authentication
{
    public record AuthResponse(
        string Id,
        string Email,
        string Fullname,
        string? Token,
        int ExpiresIn,
        string RefreshToken,
        DateTime RefreshTokenExpiration
      

        );
    
}

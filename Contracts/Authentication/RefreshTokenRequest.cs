namespace Pharmacy_managment.Contract.Authentication
{
    public record RefreshTokenRequest
    (
        string Token,
        string RefreshToken
    );
}

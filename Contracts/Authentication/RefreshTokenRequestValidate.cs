namespace Pharmacy_managment.Contract.Authentication
{
    public class RefreshTokenRequestValidate:AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidate() { 
        RuleFor(x=>x.RefreshToken).NotEmpty();
        RuleFor(x=>x.Token).NotEmpty();
        
        
        
        }
    }
}

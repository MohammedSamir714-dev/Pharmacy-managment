
using System.Text.Json;

namespace Pharmacy_managment.Auth
{
    public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions = options.Value;
        public (string token, int expirIn) GenerateToken(ApplicationUser user, IEnumerable<string> roles)
        {
            var  claims = new List<Claim>() {
                new(JwtRegisteredClaimNames.Sub,user.Id),
                new(JwtRegisteredClaimNames.Email,user.Email!),
                new(JwtRegisteredClaimNames.GivenName,user.FullName!),
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
              new(nameof(roles), JsonSerializer.Serialize(roles), JsonClaimValueTypes.JsonArray)
                };
            
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var singingCredentials=new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);
            var expiresIn =_jwtOptions.ExpiryMinutes;
            var expiration=DateTime.UtcNow.AddMinutes(expiresIn);
            var JwtToken = new JwtSecurityToken(
                issuer:_jwtOptions.Issure,
                audience: _jwtOptions.Audience,
                claims:claims,
                expires:expiration,
                signingCredentials:singingCredentials
                );
            return (token: new JwtSecurityTokenHandler().WriteToken(JwtToken), expiresIn: expiresIn*60);
        }

        public string? ValidateToken(string token)
        {
            var tokenHandler=new JwtSecurityTokenHandler();
            var symmetricSecurityKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            try
            {
                tokenHandler.ValidateToken (token, new TokenValidationParameters
                {
                    IssuerSigningKey = symmetricSecurityKey,
                    ValidateIssuerSigningKey=true,
                    ValidateIssuer=false,
                    ValidateAudience=false,
                    ClockSkew=TimeSpan.Zero

                },out SecurityToken validatedToken);
                var JwtToken = (JwtSecurityToken)validatedToken;
                return JwtToken.Claims.First(x=>x.Type== JwtRegisteredClaimNames.Sub).Value;
            }
            catch
            {
                return null;
            }
        }
    }
}

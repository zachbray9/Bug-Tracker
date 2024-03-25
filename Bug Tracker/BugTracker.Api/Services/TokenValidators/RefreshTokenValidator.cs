using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BugTracker.Api.Services.TokenValidators
{
    public class RefreshTokenValidator
    {
        private readonly IConfiguration Configuration;

        public RefreshTokenValidator(IConfiguration configuration)
        {

            Configuration = configuration;
        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtRefreshTokenKey"])),
                ValidIssuer = "https://agileproapi.azurewebsites.net",
                ValidAudience = "https://agilepro.azurewebsites.net",
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            try
            {
                handler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}

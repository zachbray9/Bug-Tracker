using BugTracker.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BugTracker.Api.Services.TokenGenerators
{
    public class AccessTokenGenerator
    {
        private readonly string JwtAuthenticationKey;

        public AccessTokenGenerator(string jwtAuthenticationKey)
        {

            JwtAuthenticationKey = jwtAuthenticationKey;

        }

        public string GenerateAccessToken(User user)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtAuthenticationKey));
            SigningCredentials credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email.ToString()),
            };

            JwtSecurityToken token = new JwtSecurityToken("https://agileproapi.azurewebsites.net", "https://agilepro.azurewebsites.net", claims, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(30));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

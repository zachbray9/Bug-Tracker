using BugTracker.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BugTracker.Api.Services.TokenGenerators
{
    public class AccessTokenGenerator
    {
        private readonly IConfiguration Configuration;
        private readonly TokenGenerator TokenGenerator;
        private readonly string JwtAccessTokenKey;

        public AccessTokenGenerator(IConfiguration configuration, TokenGenerator tokenGenerator)
        {
            Configuration = configuration;
            TokenGenerator = tokenGenerator;

            JwtAccessTokenKey = Configuration["JwtAccessTokenKey"];
        }

        public string GenerateAccessToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email.ToString()),
            };

            return TokenGenerator.GenerateToken(JwtAccessTokenKey, "https://agileproapi.azurewebsites.net", "https://agilepro.azurewebsites.net", 0.25, claims);
        }
    }
}

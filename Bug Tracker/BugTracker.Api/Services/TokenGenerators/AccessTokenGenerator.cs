using BugTracker.Api.Models.Config;
using BugTracker.Domain.Models;
using System.Security.Claims;

namespace BugTracker.Api.Services.TokenGenerators
{
    public class AccessTokenGenerator
    {
        private readonly TokenGenerator TokenGenerator;
        private readonly string JwtAccessTokenKey;

        public AccessTokenGenerator(AuthenticationConfiguration configuration, TokenGenerator tokenGenerator)
        {
            TokenGenerator = tokenGenerator;
            JwtAccessTokenKey = configuration.JwtAccessTokenKey;
        }

        public string GenerateAccessToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email.ToString()),
            };

            return TokenGenerator.GenerateToken(JwtAccessTokenKey, "https://agileproapi.azurewebsites.net", "https://agilepro.azurewebsites.net", 15, claims);
        }
    }
}

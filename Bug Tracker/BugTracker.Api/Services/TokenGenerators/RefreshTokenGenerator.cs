using BugTracker.Api.Models.Config;

namespace BugTracker.Api.Services.TokenGenerators
{
    public class RefreshTokenGenerator
    {
        private readonly TokenGenerator TokenGenerator;
        private readonly string JwtRefreshTokenKey;

        public RefreshTokenGenerator(AuthenticationConfiguration configuration, TokenGenerator tokenGenerator)
        {
            TokenGenerator = tokenGenerator;

            JwtRefreshTokenKey = configuration.JwtRefreshTokenKey;
        }

        public string GenerateRefreshToken()
        {
            return TokenGenerator.GenerateToken(JwtRefreshTokenKey, "https://agileproapi.azurewebsites.net", "https://agilepro.azurewebsites.net", 131400);
        }
    }
}

namespace BugTracker.Api.Services.TokenGenerators
{
    public class RefreshTokenGenerator
    {
        private readonly IConfiguration Configuration;
        private readonly TokenGenerator TokenGenerator;
        private readonly string JwtRefreshTokenKey;

        public RefreshTokenGenerator(IConfiguration configuration, TokenGenerator tokenGenerator)
        {
            Configuration = configuration;
            TokenGenerator = tokenGenerator;

            JwtRefreshTokenKey = Configuration["JwtRefreshTokenKey"];
        }

        public string GenerateRefreshToken()
        {
            return TokenGenerator.GenerateToken(JwtRefreshTokenKey, "https://agileproapi.azurewebsites.net", "https://agilepro.azurewebsites.net", 131400);
        }
    }
}

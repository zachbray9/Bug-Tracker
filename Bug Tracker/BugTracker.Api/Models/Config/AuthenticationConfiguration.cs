namespace BugTracker.Api.Models.Config
{
    public class AuthenticationConfiguration
    {
        public string JwtAccessTokenKey { get; set; } = string.Empty;
        public string JwtRefreshTokenKey { get; set; } = string.Empty;

        public AuthenticationConfiguration(string jwtAccessTokenKey, string jwtRefreshTokenKey)
        {
            JwtAccessTokenKey = jwtAccessTokenKey;
            JwtRefreshTokenKey = jwtRefreshTokenKey;
        }
    }
}

using BugTracker.Api.Models.Responses;
using BugTracker.Api.Services.TokenDbServices;
using BugTracker.Api.Services.TokenGenerators;
using BugTracker.Domain.Models;
using BugTracker.Domain.Models.Auth;

namespace BugTracker.Api.Services.Authenticators
{
    public class Authenticator
    {
        private readonly AccessTokenGenerator AccessTokenGenerator;
        private readonly RefreshTokenGenerator RefreshTokenGenerator;
        private readonly RefreshTokenService RefreshTokenService;

        public Authenticator(AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator, RefreshTokenService refreshTokenService)
        {
            AccessTokenGenerator = accessTokenGenerator;
            RefreshTokenGenerator = refreshTokenGenerator;
            RefreshTokenService = refreshTokenService;
        }

        public async Task<AuthenticatedUserResponse> Authenticate(User user)
        {
            string accessToken = AccessTokenGenerator.GenerateAccessToken(user);
            string refreshToken = RefreshTokenGenerator.GenerateRefreshToken();

            RefreshToken refreshTokenDTO = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
            };

            await RefreshTokenService.Create(refreshTokenDTO);

            return new AuthenticatedUserResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}

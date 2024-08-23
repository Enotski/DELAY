using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Contracts.Models.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace DELAY.Infrastructure.Auth.Services
{
    internal class JwtTokensService : ITokensService
    {
        private readonly JwtGenerator _tokenGenerator;
        public JwtTokensService(IOptions<TokensSettings> jwtSettings)
        {
            ArgumentNullException.ThrowIfNull(jwtSettings);

            _tokenGenerator = new JwtGenerator(jwtSettings.Value);
        }

        public TokenValidationParameters GetTokenValidationParameters() => _tokenGenerator.CreateTokenValidationParameters();

        public bool ValidateToken(string token)
        {
            return _tokenGenerator.ValidateToken(token.Replace("Bearer", "").Trim());
        }
        public ClaimsPrincipal GetPrincipal(string token)
        {
            return _tokenGenerator.GetPrincipal(token.Replace("Bearer", "").Trim());
        }
        public Tokens CreateTokens(Guid userId, string userLogin)
        {
            var access_token = _tokenGenerator.CreateAccessToken(userId, userLogin);
            var refresh_token = _tokenGenerator.CreateRefreshToken();

            return new Tokens(access_token, refresh_token);
        }
    }
}

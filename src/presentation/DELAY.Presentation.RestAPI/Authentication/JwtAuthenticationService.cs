using DELAY.Core.Application.Abstractions.Services;
using DELAY.Core.Application.Contracts.Models;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace DELAY.Infrastructure.Authentication
{
    internal class JwtAuthenticationService
    {
        private readonly JwtGenerator _tokenGenerator;
        public JwtAuthenticationService(IOptions<TokensSettings> jwtSettings)
        {
            ArgumentNullException.ThrowIfNull(jwtSettings);

            _tokenGenerator = new JwtGenerator(jwtSettings.Value);
        }
        public bool ValidateToken(string token)
        {
            return _tokenGenerator.ValidateToken(token.Replace("Bearer", "").Trim());
        }
        public ClaimsPrincipal GetPrincipal(string token)
        {
            return _tokenGenerator.GetPrincipal(token.Replace("Bearer", "").Trim());
        }
        public TokensModel CreateTokens(Guid userId, string userLogin)
        {
            var access_token = _tokenGenerator.CreateAccessToken(userId, userLogin);
            var refresh_token = _tokenGenerator.CreateRefreshToken();

            return new TokensModel(access_token, refresh_token);
        }
    }
}

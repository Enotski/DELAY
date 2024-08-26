using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Contracts.Models.Auth;
using Microsoft.Extensions.Options;
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

        public bool IsValidToken(string token) =>
            _tokenGenerator.ValidateToken(token.Replace("Bearer", "").Trim());

        public ClaimsPrincipal GetPrincipal(string token) =>
            _tokenGenerator.GetPrincipal(token.Replace("Bearer", "").Trim());

        public Tokens CreateTokens(Guid userId, string name, string email, string phone, string role) =>
            new Tokens(_tokenGenerator.CreateAccessToken(userId, name, email, phone, role), _tokenGenerator.CreateRefreshToken(userId));
    }
}

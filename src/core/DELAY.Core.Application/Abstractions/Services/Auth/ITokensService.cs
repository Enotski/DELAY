using DELAY.Core.Application.Contracts.Models.Auth;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace DELAY.Core.Application.Abstractions.Services.Auth
{
    public interface ITokensService
    {
        bool ValidateToken(string token);

        ClaimsPrincipal GetPrincipal(string token);

        Tokens CreateTokens(Guid userId, string userLogin);

        TokenValidationParameters GetTokenValidationParameters();
    }
}

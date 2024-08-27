using DELAY.Core.Application.Contracts.Models.Auth;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace DELAY.Core.Application.Abstractions.Services.Auth
{
    public interface ITokensService
    {
        bool IsValidToken(string token);

        ClaimsPrincipal GetPrincipal(string token, out DateTime validTo);

        Tokens CreateTokens(Guid userId, string name, string email, string phone, string role);
    }
}

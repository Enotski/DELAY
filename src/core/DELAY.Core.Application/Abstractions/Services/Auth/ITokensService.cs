using DELAY.Core.Application.Contracts.Models.Auth;
using System.Security.Claims;

namespace DELAY.Core.Application.Abstractions.Services.Auth
{
    /// <summary>
    /// Service of tokens methods
    /// </summary>
    public interface ITokensService
    {
        /// <summary>
        /// Is token correct
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool IsValidToken(string token);

        /// <summary>
        /// Get user info from token
        /// </summary>
        /// <param name="token"></param>
        /// <param name="validTo"></param>
        /// <returns></returns>
        ClaimsPrincipal GetPrincipal(string token, out DateTime validTo);

        /// <summary>
        /// Generate tokens
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="phone"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        Tokens CreateTokens(Guid userId, string name, string email, string phone, string role);
    }
}

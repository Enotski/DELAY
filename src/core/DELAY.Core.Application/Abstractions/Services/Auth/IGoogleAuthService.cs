using DELAY.Core.Application.Contracts.Models.Auth;

namespace DELAY.Core.Application.Abstractions.Services.Auth
{
    /// <summary>
    /// Authentication via goole
    /// </summary>
    public interface IGoogleAuthService
    {
        /// <summary>
        /// Get profile info
        /// </summary>
        /// <param name="singleUseExchangeCode"></param>
        /// <returns></returns>
        Task<GoogleUserCredentials> GetUserCredentialsByCodeAsync(string singleUseExchangeCode);
    }
}

using DELAY.Core.Application.Contracts.Models.Auth;

namespace DELAY.Core.Application.Abstractions.Services.Auth
{
    /// <summary>
    /// Service for authentication via vk 
    /// </summary>
    public interface IVkAuthService
    {
        /// <summary>
        /// Get profile info
        /// </summary>
        /// <param name="singleUseExchangeCode"></param>
        /// <param name="deviceId"></param>
        /// <param name="codeVerifier"></param>
        /// <returns></returns>
        Task<VkUserCredentials> GetUserCredentialsByCodeAsync(string singleUseExchangeCode, string deviceId, string codeVerifier);
    }
}

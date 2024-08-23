using DELAY.Core.Application.Contracts.Models.Auth;

namespace DELAY.Core.Application.Abstractions.Services.Auth
{
    public interface IVkAuthService
    {
        Task<VkUserCredentials> GetUserCredentialsByCodeAsync(string singleUseExchangeCode, string deviceId, string clientId, string codeVerifier);
    }
}

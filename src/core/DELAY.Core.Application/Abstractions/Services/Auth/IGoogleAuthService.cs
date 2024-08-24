using DELAY.Core.Application.Contracts.Models.Auth;

namespace DELAY.Core.Application.Abstractions.Services.Auth
{
    public interface IGoogleAuthService
    {
        Task<GoogleUserCredentials> GetUserCredentialsByCodeAsync(string singleUseExchangeCode);
    }
}

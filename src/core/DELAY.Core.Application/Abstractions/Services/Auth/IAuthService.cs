using DELAY.Core.Application.Contracts.Models.Auth;

namespace DELAY.Core.Application.Abstractions.Services.Auth
{
    public interface IAuthService
    {
        Task<Tokens> RefreshTokensAsync(string refreshToken);

        Task<AuthResult> SignInAsync(SignInRequest model);

        Task<AuthResult> SignUpAsync(SignUpRequest model);

        Task<AuthResult> SignInGoogleAsync(GoogleAuthRequest model);

        Task<AuthResult> SignInVkAsync(VkAuthRequest model);

        void SignOut(string refreshToken);

        void SignOutAll(string refreshToken);
    }
}

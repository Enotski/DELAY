using DELAY.Core.Application.Contracts.Models.Auth;

namespace DELAY.Core.Application.Abstractions.Services.Auth
{
    /// <summary>
    /// Athentication service
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Silent login by refresh token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AuthResult> TransientSingInByRefreshTokenAsync(string refreshToken, AuthUserAgentRequest model);

        /// <summary>
        /// Update tokens
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        Task<Tokens> RefreshTokensAsync(string refreshToken);

        /// <summary>
        /// Log in by form model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AuthResult> SignInAsync(SignInRequest model);

        /// <summary>
        /// Register by form model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AuthResult> SignUpAsync(SignUpRequest model);

        /// <summary>
        /// Sign in by google profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AuthResult> SignInGoogleAsync(GoogleAuthRequest model);

        /// <summary>
        /// Sign in by vk profile
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<AuthResult> SignInVkAsync(VkAuthRequest model);

        /// <summary>
        /// Log out
        /// </summary>
        /// <param name="refreshToken"></param>
        void SignOut(string refreshToken);

        /// <summary>
        /// Log out from all sessions
        /// </summary>
        /// <param name="refreshToken"></param>
        void SignOutAll(string refreshToken);
    }
}

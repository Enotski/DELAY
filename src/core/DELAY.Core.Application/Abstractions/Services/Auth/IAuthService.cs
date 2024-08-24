﻿using DELAY.Core.Application.Contracts.Models.Auth;

namespace DELAY.Core.Application.Abstractions.Services.Auth
{
    public interface IAuthService
    {
        Tokens RefreshTokens(string refreshToken);

        Task<AuthResult> SignInAsync(SignInRequest model);

        Task<AuthResult> SignUpAsync(SignUpRequest model);

        Task<AuthResult> SignInGoogleAsync(GoogleAuthRequest model);

        Task<AuthResult> SignInVkAsync(VkAuthRequest model);

        Task SignOut();
    }
}

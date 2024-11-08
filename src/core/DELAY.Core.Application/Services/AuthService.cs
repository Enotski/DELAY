﻿using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Abstractions.Services.Common;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Auth;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;

namespace DELAY.Core.Application.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IGoogleAuthService _googleAuthService;

        private readonly IVkAuthService _vkAuthService;

        private readonly IPasswordHelper _passwordHelper;

        private readonly ICacheService _cacheService;

        private readonly ITokensService _tokensService;

        private readonly IUserStorage _userStorage;

        public AuthService(IGoogleAuthService googleAuthService, IVkAuthService vkAuthService, IPasswordHelper passwordHelper, ICacheService cacheService, ITokensService tokensService, IUserStorage userStorage)
        {
            _googleAuthService = googleAuthService;
            _vkAuthService = vkAuthService;
            _passwordHelper = passwordHelper;
            _cacheService = cacheService;
            _tokensService = tokensService;
            _userStorage = userStorage;
        }

        public async Task<AuthResult> TransientSingInByRefreshTokenAsync(string refreshToken, AuthUserAgentRequest model)
        {
            if (!_tokensService.IsValidToken(refreshToken))
                return null;

            var cachedSession = _cacheService.GetData<SessionCache>(refreshToken);

            if (cachedSession == null)
                return null;

            _cacheService.RemoveData(cachedSession.RefreshToken);

            if (!_tokensService.IsValidToken(cachedSession.RefreshToken))
            {
                return null;
            }

            var user = await _userStorage.GetAsync(cachedSession.UserId);

            return CreateTokensAndSetSession(user, model, AuthProviderType.Internal);
        }

        public async Task<Tokens> RefreshTokensAsync(string refreshToken)
        {
            if (!_tokensService.IsValidToken(refreshToken))
                return null;

            var cachedSession = _cacheService.GetData<SessionCache>(refreshToken);

            if (cachedSession == null)
                return null;

            _cacheService.RemoveData(cachedSession.RefreshToken);

            if (!_tokensService.IsValidToken(cachedSession.RefreshToken))
            {
                return null;
            }

            var user = await _userStorage.GetAsync(cachedSession.UserId);

            var tokens = _tokensService.CreateTokens(user.Id, user.DisplayName, user.Email, user.PhoneNumber, user.Role.ToString());

            SetUserSessionAsync(tokens.RefreshToken, user, new AuthUserAgentRequest(cachedSession.Fingerprint, cachedSession.IpAddress, cachedSession.UserAgent), cachedSession.AuthProvider);

            return tokens;
        }

        public async Task<AuthResult> SignInAsync(SignInRequest model)
        {
            User user;

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                user = await _userStorage.GetByEmailAsync(model.Email);
            }
            else
            {
                user = await _userStorage.GetByPhoneAsync(model.Phone);
            }

            if (user == null || !_passwordHelper.IsEqual(model.Password, user.Password))
            {
                throw new ArgumentException("Wrong login or password");
            }

            return CreateTokensAndSetSession(user, model, AuthProviderType.Internal);
        }

        public async Task<AuthResult> SignUpAsync(SignUpRequest model)
        {
            var user = new User(model.Name, model.Email, model.Phone, _passwordHelper.GetHash(model.Password));

            await ValidateUserAsync(user, true);

            user.Id = await _userStorage.AddAsync(user);

            return CreateTokensAndSetSession(user, model, AuthProviderType.Internal);
        }

        public async Task<AuthResult> SignInGoogleAsync(GoogleAuthRequest model)
        {
            var res = await _googleAuthService.GetUserCredentialsByCodeAsync(model.Code);

            return await AuthenticateUserFromExternalService(res, model, AuthProviderType.Google);
        }

        public async Task<AuthResult> SignInVkAsync(VkAuthRequest model)
        {
            var res = await _vkAuthService.GetUserCredentialsByCodeAsync(model.Code, model.DeviceId, model.CodeVerifier);

            return await AuthenticateUserFromExternalService(res, model, AuthProviderType.Vk);
        }

        public void SignOut(string refreshToken)
        {
            var cachedSession = _cacheService.GetData<SessionCache>(refreshToken);

            if (cachedSession != null)
            {
                _cacheService.RemoveData(cachedSession.RefreshToken);
            }

            var root = _tokensService.GetPrincipal(refreshToken, out DateTime validTo)?.Claims?.FirstOrDefault(x => x.Type == "ueid");

            var cachedRootSession = _cacheService.GetData<RootUserSessionCache>(root.Value);

            if (cachedRootSession != null)
            {
                cachedRootSession.SessionsKeys = cachedRootSession.SessionsKeys.Where(x => x != refreshToken);

                _cacheService.SetData(root.Value, cachedRootSession);
            }
        }

        public void SignOutAll(string refreshToken)
        {
            var cachedSession = _cacheService.GetData<SessionCache>(refreshToken);

            if (cachedSession != null)
            {
                _cacheService.RemoveData(cachedSession.RefreshToken);
            }

            var root = _tokensService.GetPrincipal(refreshToken, out DateTime validTo)?.Claims?.FirstOrDefault(x => x.Type == "ueid");

            var cachedRootSession = _cacheService.GetData<RootUserSessionCache>(root.Value);

            if (cachedRootSession != null)
            {
                cachedRootSession.SessionsKeys = cachedRootSession.SessionsKeys.Where(x => x != refreshToken);

                foreach (var key in cachedRootSession.SessionsKeys)
                    _cacheService.RemoveData(key);

                _cacheService.RemoveData(root.Value);
            }
        }

        private async Task<AuthResult> AuthenticateUserFromExternalService(UserExternalServiceCredentials userCreds, AuthUserAgentRequest authAgent, AuthProviderType authProvider)
        {
            User user = null;

            if (!string.IsNullOrWhiteSpace(userCreds.Email))
            {
                user = await _userStorage.GetByEmailAsync(userCreds.Email);
            }

            if (user == null)
            {
                user = new User(userCreds.GivenName, userCreds.Email, "");

                user.Id = await _userStorage.AddAsync(user);
            }

            return CreateTokensAndSetSession(user, authAgent, authProvider);
        }

        private AuthResult CreateTokensAndSetSession(User user, AuthUserAgentRequest authModel, AuthProviderType authProvider)
        {
            var tokens = _tokensService.CreateTokens(user.Id, user.DisplayName, user.Email, user.PhoneNumber, user.Role.ToString());

            SetUserSessionAsync(tokens.RefreshToken, user, authModel, authProvider);

            return new AuthResult(user.Email, user.PhoneNumber, user.DisplayName, user.Role, tokens);
        }

        private void SetUserSessionAsync(string refreshToken, User user, AuthUserAgentRequest authModel, AuthProviderType authProvider)
        {
            var tokenClaims = _tokensService.GetPrincipal(refreshToken, out DateTime validTo)?.Claims;

            var session = new SessionCache(Guid.NewGuid(), refreshToken, user.Id, authModel.Fingerprint, authModel.IpAddress, authModel.UserAgent, DateTime.UtcNow, validTo, authProvider);

            _cacheService.SetData(refreshToken, session, validTo);

            var root = tokenClaims?.FirstOrDefault(x => x.Type == "ueid");

            var cachedRootSession = _cacheService.GetData<RootUserSessionCache>(root.Value);

            if (cachedRootSession == null)
            {
                cachedRootSession = new RootUserSessionCache([refreshToken]);
            }
            else
            {
                if (cachedRootSession.SessionsKeys.Count() >= 5)
                {
                    foreach (var key in cachedRootSession.SessionsKeys)
                        _cacheService.RemoveData(key);

                    cachedRootSession.SessionsKeys = [refreshToken];
                }
                else
                {
                    cachedRootSession.SessionsKeys.Append(refreshToken);
                }
            }

            _cacheService.SetData(root.Value, cachedRootSession);
        }

        private async Task ValidateUserAsync(User user, bool isValidatePassword)
        {
            if (!user.IsValidCredentials(isValidatePassword))
                throw new ArgumentException("Invalid user data");

            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                if (!await _userStorage.IsUniqueEmail(user.Email))
                    throw new ArgumentException("Such email already exist");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(user.PhoneNumber))
                    throw new ArgumentException("Email or phone required");

                if (!await _userStorage.IsUniquePhone(user.PhoneNumber))
                    throw new ArgumentException("Such phone already exist");
            }
        }
    }
}

using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Abstractions.Services.Common;
using DELAY.Core.Application.Abstractions.Storages;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Auth;
using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models;
using Microsoft.Extensions.Options;

namespace DELAY.Core.Application.Services.Auth
{
    internal class AuthService : IAuthService
    {
        private readonly IGoogleAuthService _googleAuthService;

        private readonly IVkAuthService _vkAuthService;

        private readonly IPasswordHelper _passwordHelper;

        private readonly ICachingService _cacheService;

        private readonly ITokensService _tokensService;

        private readonly ISessionLogStorage _sessionLogStorage;

        private readonly IUserStorage _userStorage;

        private readonly TokensSettings _tokensSettings;

        public AuthService(IGoogleAuthService googleAuthService, IVkAuthService vkAuthService, IPasswordHelper passwordHelper, ICachingService cacheService, ITokensService tokensService, ISessionLogStorage sessionLogStorage, IUserStorage userStorage, IOptions<TokensSettings> tokensSettings)
        {
            _googleAuthService = googleAuthService;
            _vkAuthService = vkAuthService;
            _passwordHelper = passwordHelper;
            _cacheService = cacheService;
            _tokensService = tokensService;
            _sessionLogStorage = sessionLogStorage;
            _userStorage = userStorage;
            _tokensSettings = tokensSettings.Value;
        }

        public async Task<Tokens> RefreshTokensAsync(string refreshToken)
        {
            var cachedSession = _cacheService.GetValueFromCache<SessionCache>(refreshToken);

            if (cachedSession == null)
                return null;

            _cacheService.RemoveValueFromCache(cachedSession.RefreshToken);

            var session = await _sessionLogStorage.GetAsync(cachedSession.SessionId);

            if(session == null)
                return null;

            if (!_tokensService.IsValidToken(cachedSession.RefreshToken))
            {
                await _sessionLogStorage.DeleteAsync(cachedSession.SessionId);

                return null;
            }
                
            var tokens = _tokensService.CreateTokens();
                        
            _cacheService.SetValueToCache(tokens.RefreshToken, new SessionCache(tokens.RefreshToken, cachedSession.SessionId, cachedSession.UserId), DateTimeOffset.UtcNow.AddDays(_tokensSettings.RefreshTokenExpirationDays));

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

            return await SetUserSessionAsync(user, model.UserAgent, model.IpAddress, AuthProviderType.Internal);
        }

        public async Task<AuthResult> SignUpAsync(SignUpRequest model)
        {
            var user = new User(model.Name, model.Email, model.Phone, _passwordHelper.GetHash(model.Password));

            await ValidateUserAsync(user, true);

            user.Id = await _userStorage.AddAsync(user);

            return await SetUserSessionAsync(user, model.UserAgent, model.IpAddress, AuthProviderType.Internal);
        }

        public async Task<AuthResult> SignInGoogleAsync(GoogleAuthRequest model)
        {
            var res = await _googleAuthService.GetUserCredentialsByCodeAsync(model.Code);

            return await AuthenticateUserFromExternalService(res, model.UserAgent, model.IpAddress, AuthProviderType.Google);
        }

        public async Task<AuthResult> SignInVkAsync(VkAuthRequest model)
        {
            var res = await _vkAuthService.GetUserCredentialsByCodeAsync(model.Code, model.DeviceId, model.CodeVerifier);

            return await AuthenticateUserFromExternalService(res, model.UserAgent, model.IpAddress, AuthProviderType.Vk);
        }

        public async Task SignOutAsync(string refreshToken)
        {
            var cachedSession = _cacheService.GetValueFromCache<SessionCache>(refreshToken);

            if (cachedSession != null)
            {
                _cacheService.RemoveValueFromCache(cachedSession);

                await _sessionLogStorage.DeleteAsync(cachedSession.SessionId);
            }
        }

        public async Task SignOutAllAsync(string refreshToken)
        {
            var cachedSession = _cacheService.GetValueFromCache<SessionCache>(refreshToken);

            if (cachedSession != null)
            {
                _cacheService.RemoveValueFromCache(cachedSession);

                await _sessionLogStorage.DeleteByUserAsync(cachedSession.UserId);
            }
        }

        private async Task<AuthResult> AuthenticateUserFromExternalService(UserExternalServiceCredentials userCreds, string userAgent, string ipAddress, AuthProviderType authProvider)
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

            return await SetUserSessionAsync(user, userAgent, ipAddress, authProvider);
        }

        private async Task<AuthResult> SetUserSessionAsync(User user, string userAgent, string ip, AuthProviderType authProvider)
        {
            var tokens = _tokensService.CreateTokens();

            var sessionId = await AddSessionAsync(user.Id, userAgent, ip, authProvider);

            _cacheService.SetValueToCache(tokens.RefreshToken, new SessionCache(tokens.RefreshToken, sessionId, user.Id), DateTimeOffset.UtcNow.AddDays(_tokensSettings.RefreshTokenExpirationDays));

            return new AuthResult(user.Email, user.PhoneNumber, user.DisplayName, user.Role, tokens);
        }

        private async Task<Guid> AddSessionAsync(Guid userId, string userAgent, string ip, AuthProviderType authProvider)
        {
            var session = await _sessionLogStorage.GetSessionAsync(userId, ip, userAgent);

            if (session == null || session.IpAddress != ip || session.UserAgent != userAgent || session.AuthProvider != authProvider)
            {
                session = new SessionLog(userId, userAgent, ip, DateTime.UtcNow, authProvider);

                session.Id = await _sessionLogStorage.AddAsync(session);
            }
            else
            {
                session.Update(authProvider);

                await _sessionLogStorage.UpdateAsync(session);
            }

            return session.Id;
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

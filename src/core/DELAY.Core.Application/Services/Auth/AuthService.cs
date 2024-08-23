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

        private readonly IAccountStorage _accountStorage;

        private readonly ISessionLogStorage _sessionLogStorage;

        private readonly IUserStorage _userStorage;

        private readonly TokensSettings _tokensSettings;

        private readonly GoogleApiSecrets _googleApiSecrets;

        private readonly VkApiSecrets _vkApiSecrets;

        public AuthService(IGoogleAuthService googleAuthService, IVkAuthService vkAuthService, IPasswordHelper passwordHelper, ICachingService cacheService, ITokensService tokensService, IAccountStorage accountStorage, ISessionLogStorage sessionLogStorage, IUserStorage userStorage, IOptions<TokensSettings> tokensSettings, IOptions<GoogleApiSecrets> googleApiSecrets, IOptions<VkApiSecrets> vkApiSecrets)
        {
            _googleAuthService = googleAuthService;
            _vkAuthService = vkAuthService;
            _passwordHelper = passwordHelper;
            _cacheService = cacheService;
            _tokensService = tokensService;
            _accountStorage = accountStorage;
            _sessionLogStorage = sessionLogStorage;
            _userStorage = userStorage;
            _tokensSettings = tokensSettings.Value;
            _googleApiSecrets = googleApiSecrets.Value;
            _vkApiSecrets = vkApiSecrets.Value;
        }

        public Task<Tokens> RefreshTokensAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthResult> SignInAsync(SignInRequest model)
        {
            User user;

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                user = await _accountStorage.GetByEmailAsync(model.Email);
            }
            else
            {
                user = await _accountStorage.GetByEmailAsync(model.Phone);
            }

            if (user == null || !_passwordHelper.IsEqual(model.Password, user.Password))
            {
                throw new ArgumentException("Wrong login or password");
            }

            return await AuthUserSessionAsync(user);
        }

        public async Task<AuthResult> SignUpAsync(SignUpRequest model)
        {
            var user = new User(model.Name, model.Email, model.Phone, _passwordHelper.GetHash(model.Password));

            await ValidateUserAsync(user);

            user.Id = await _userStorage.AddAsync(user);

            return await AuthUserSessionAsync(user);
        }

        public async Task<AuthResult> SignInGoogleAsync(GoogleAuthRequest model)
        {
            var res = await _googleAuthService.GetUserCredentialsByCodeAsync(model.Code, _googleApiSecrets.ClientId, _googleApiSecrets.ClientSecret);
            throw new NotImplementedException();
        }

        public async Task<AuthResult> SignInVkAsync(VkAuthRequest model)
        {
            var res = await _vkAuthService.GetUserCredentialsByCodeAsync(model.Code, model.DeviceId, _vkApiSecrets.ClientId, model.CodeVerifier);
            throw new NotImplementedException();
        }

        private async Task<AuthResult> AuthUserSessionAsync(User user)
        {
            var tokens = _tokensService.CreateTokens(user.Id, user.Name);

            var sessionId = await AddSessionAsync(user.Id, "", "", tokens.RefreshToken, AuthProviderType.Internal);

            _cacheService.SetValueToCache(tokens.RefreshToken, new SessionCache(tokens.RefreshToken, sessionId, user.Id), DateTimeOffset.UtcNow.AddDays(_tokensSettings.RefreshTokenExpirationDays));

            return new AuthResult(user.Id, user.Name, user.Role, tokens);
        }

        private async Task<Guid> AddSessionAsync(Guid userId, string userAgent, string ip, string refreshToken, AuthProviderType authProvider)
        {

            var session = await _sessionLogStorage.GetSession(userId, ip, userAgent);

            if (session == null || session.Ip != ip || session.UserAgent != userAgent || session.AuthProvider != authProvider)
            {
                session = new SessionLog(userId, userAgent, ip, DateTime.UtcNow, authProvider);

                session.Id = await _sessionLogStorage.AddAsync(session);
            }
            else
            {
                session.Update();

                await _sessionLogStorage.UpdateAsync(session);
            }

            return session.Id;
        }

        private async Task ValidateUserAsync(User user)
        {
            if (!user.IsValidCredentials())
                throw new ArgumentException("Invalid user data");

            if (!string.IsNullOrWhiteSpace(user.Email) && !await _userStorage.IsUniqueEmail(user.Email))
                throw new ArgumentException("Such email already exist");

            else if (!await _userStorage.IsUniquePhone(user.PhoneNumber))
                throw new ArgumentException("Such phone already exist");
        }
    }
}

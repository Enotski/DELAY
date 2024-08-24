using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Contracts.Models.Auth;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2.Flows;
using Microsoft.Extensions.Options;

namespace DELAY.Infrastructure.Auth.Services
{
    internal class GoogleAuthService : IGoogleAuthService
    {
        private readonly GoogleApiSecrets _googleApiSecrets;

        public GoogleAuthService(IOptions<GoogleApiSecrets> googleApiSecrets)
        {
            _googleApiSecrets = googleApiSecrets.Value;
        }

        public async Task<GoogleUserCredentials> GetUserCredentialsByCodeAsync(string singleUseExchangeCode)
        {
            var initializer = new AuthorizationCodeFlow.Initializer("https://localhost", "https://oauth2.googleapis.com/token")
            {
                ClientSecrets = new Google.Apis.Auth.OAuth2.ClientSecrets()
                {
                    ClientId = _googleApiSecrets.ClientId,
                    ClientSecret = _googleApiSecrets.ClientSecret
                },
            };

            using var flow = new AuthorizationCodeFlow(initializer);

            var tokens = await flow.ExchangeCodeForTokenAsync(Guid.NewGuid().ToString(), singleUseExchangeCode, "https://localhost", CancellationToken.None);

            var creds = await GoogleJsonWebSignature.ValidateAsync(tokens.IdToken);

            return new GoogleUserCredentials(creds.Email, creds.Name, creds.GivenName, creds.FamilyName);
        }
    }
}

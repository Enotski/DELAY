using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Contracts.Models.Auth;
using Google.Apis.Auth;
using Google.Apis.Auth.OAuth2.Flows;

namespace DELAY.Infrastructure.Auth.Services
{
    internal class GoogleAuthService : IGoogleAuthService
    {
        public async Task<GoogleUserCredentials> GetUserCredentialsByCodeAsync(string singleUseExchangeCode, string clientId, string clientSecret)
        {
            var initializer = new AuthorizationCodeFlow.Initializer("https://localhost", "https://oauth2.googleapis.com/token")
            {
                ClientSecrets = new Google.Apis.Auth.OAuth2.ClientSecrets()
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
            };

            using var flow = new AuthorizationCodeFlow(initializer);

            var tokens = await flow.ExchangeCodeForTokenAsync(Guid.NewGuid().ToString(), singleUseExchangeCode, "https://localhost", CancellationToken.None);

            var creds = await GoogleJsonWebSignature.ValidateAsync(tokens.IdToken);

            return new GoogleUserCredentials(creds.Email, creds.Name, creds.GivenName, creds.FamilyName);
        }
    }
}

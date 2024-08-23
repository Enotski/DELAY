using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Contracts.Models.Auth;
using DELAY.Infrastructure.Auth.Models;
using System.Net.Http.Json;
using System.Web;

namespace DELAY.Infrastructure.Auth.Services
{
    internal class VkAuthService : IVkAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VkAuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<VkUserCredentials> GetUserCredentialsByCodeAsync(string singleUseExchangeCode, string deviceId, string clientId, string codeVerifier)
        {
            using var client = _httpClientFactory.CreateClient();

            var queryParams = new Dictionary<string, string>()
            {
                { "grant_type", "authorization_code" },
                { "client_id", clientId },
                { "state", Guid.NewGuid().ToString().Replace("-", "") },
                { "code_verifier", codeVerifier },
                { "redirect_uri", "https://localhost:443" },
                { "device_id", deviceId },
            };

            string uri = "https://id.vk.com/oauth2/auth?" + string.Join("&", queryParams.Select(x => $"{HttpUtility.UrlEncode(x.Key)}={HttpUtility.UrlEncode(x.Value)}"));

            using var content = new FormUrlEncodedContent([new KeyValuePair<string, string>("code", singleUseExchangeCode)]);

            using var response = await client.PostAsync(uri, content);

            var authResult = await response.Content.ReadFromJsonAsync<VkApiCodeExchangeResponse>();

            using var getUserContent = new FormUrlEncodedContent([new KeyValuePair<string, string>("client_id", clientId), new KeyValuePair<string, string>("access_token", authResult.access_token)]);

            using var userDataResponse = await client.PostAsync("https://id.vk.com/oauth2/user_info", getUserContent);

            var userData = await userDataResponse.Content.ReadFromJsonAsync<VkUserInfoResponse>();

            return new VkUserCredentials(userData.user.email, userData.user.first_name, userData.user.first_name + " " + userData.user.last_name, userData.user.last_name);
        }
    }
}

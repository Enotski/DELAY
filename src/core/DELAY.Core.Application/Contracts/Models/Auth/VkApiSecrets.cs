namespace DELAY.Core.Application.Contracts.Models.Auth
{
    /// <summary>
    /// Secrets of vk services
    /// </summary>
    public class VkApiSecrets
    {
        public const string SectionName = "Authentication:Vk";

        public VkApiSecrets()
        {
        }

        public VkApiSecrets(string clientSecret, string clientId, string serviceSecret)
        {
            ClientSecret = clientSecret;
            ClientId = clientId;
            ServiceSecret = serviceSecret;
        }

        public string ClientSecret { get; set; }

        public string ClientId { get; set; }

        public string ServiceSecret { get; set; }
    }
}

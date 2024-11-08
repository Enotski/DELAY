namespace DELAY.Core.Application.Contracts.Models.Auth
{
    /// <summary>
    /// Secrets of google api
    /// </summary>
    public class GoogleApiSecrets
    {
        public const string SectionName = "Authentication:Google";

        public GoogleApiSecrets()
        {
        }

        public GoogleApiSecrets(string clientSecret, string clientId)
        {
            ClientSecret = clientSecret;
            ClientId = clientId;
        }

        public string ClientSecret { get; set; }

        public string ClientId { get; set; }
    }
}

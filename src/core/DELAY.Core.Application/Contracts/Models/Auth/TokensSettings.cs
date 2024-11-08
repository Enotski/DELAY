namespace DELAY.Core.Application.Contracts.Models.Auth
{
    /// <summary>
    /// Configuration of tokens
    /// </summary>
    public class TokensSettings
    {
        /// <summary>
        /// Section name in config file
        /// </summary>
        public const string SectionName = "TokensSettings";

        /// <summary>
        /// Scheme
        /// </summary>
        public string SchemeName { get; set; }

        /// <summary>
        /// Token issuer name
        /// </summary>
        public string Issuer { get; init; } = string.Empty;

        /// <summary>
        /// Token audience name
        /// </summary>
        public string Audience { get; init; } = string.Empty;

        /// <summary>
        /// Token key
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;

        /// <summary>
        /// Expiration access token time
        /// </summary>
        public uint AccessTokenExpirationMinutes { get; init; } = 0;

        /// <summary>
        /// Expiration refresh token time
        /// </summary>
        public uint RefreshTokenExpirationDays { get; init; } = 0;

        public TokensSettings() { }

        public TokensSettings(string schemeName, string issuer, string audience, string secretKey, uint accessTokenExpirationMinutes, uint refreshTokenExpirationDays)
        {
            SchemeName = schemeName;
            Issuer = issuer;
            Audience = audience;
            SecretKey = secretKey;
            AccessTokenExpirationMinutes = accessTokenExpirationMinutes;
            RefreshTokenExpirationDays = refreshTokenExpirationDays;
        }
    }
}

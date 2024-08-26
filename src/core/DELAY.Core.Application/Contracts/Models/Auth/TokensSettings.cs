﻿namespace DELAY.Core.Application.Contracts.Models.Auth
{
    public class TokensSettings
    {
        /// <summary>
        /// Название раздела
        /// </summary>
        public const string SectionName = "TokensSettings";
        /// <summary>
        /// Название схемы
        /// </summary>
        public string SchemeName { get; set; }
        /// <summary>
        /// Приложение, из которого отправляется токен
        /// </summary>
        public string Issuer { get; init; } = string.Empty;
        /// <summary>
        /// Потребитель токена
        /// </summary>
        public string Audience { get; init; } = string.Empty;
        /// <summary>
        /// Секретный ключ
        /// </summary>
        public string SecretKey { get; set; } = string.Empty;
        /// <summary>
        /// Идентификатор сведений о минутах истечения срока действия Access токена.
        /// </summary>
        public uint AccessTokenExpirationMinutes { get; init; } = 0;
        /// <summary>
        /// Идентификатор сведений о днях истечения срока действия Refresh токена.
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DELAY.Infrastructure.Authentication
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
        public string SecretKey { get; init; } = string.Empty;
        /// <summary>
        /// Идентификатор сведений о минутах истечения срока действия Access токена.
        /// </summary>
        public int AccessTokenExpirationMinutes { get; init; } = 0;
        /// <summary>
        /// Идентификатор сведений о днях истечения срока действия Refresh токена.
        /// </summary>
        public int RefreshTokenExpirationDays { get; init; } = 0;

        public TokensSettings() { }

        public TokensSettings(string schemeName, string issuer, string audience, string secretKey, int accessTokenExpirationMinutes, int refreshTokenExpirationDays)
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

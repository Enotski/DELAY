using DELAY.Core.Application.Contracts.Models.Auth;
using DELAY.Core.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;

namespace DELAY.Infrastructure.Auth.Services
{
    public class JwtGenerator
    {
        /// <summary>
        /// Настройки jwt
        /// </summary>
        private readonly TokensSettings _jwtSettings;
        /// <summary>
        /// Срок истечения срока валидности токена доступа
        /// </summary>
        /// <returns></returns>
        private DateTime AccessTokenExpirationTime { get => DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes); }
        /// <summary>
        /// Срок истечения срока валидности токена обновления
        /// </summary>
        /// <returns></returns>
        private DateTime RefreshTokenExpirationTime { get => DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays); }
        /// <summary>
        ///  Инициализирует новый экземпляр класса <see cref="JwtGenerator"/> .
        /// </summary> 
        /// <param name="jwtSettings">Настройки jwt.</param>
        /// <summary>
        /// Генерация секретного ключа
        /// </summary>
        /// <param name="key">Основной ключ</param>
        /// <returns></returns>
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key) => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        public static TokenValidationParameters CreateTokenValidationParameters(string key, string issuer, string audience)
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = JwtGenerator.GetSymmetricSecurityKey(key),
                LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) => expires >= DateTime.UtcNow,
            };
        }

        public JwtGenerator(TokensSettings jwtSettings)
        {
            _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(TokensSettings));
        }
        /// <summary>
        /// Генерация jwt токена
        /// </summary>
        /// <param name="issuer">Эмитент</param>
        /// <param name="audience">Потребитель</param>
        /// <param name="expires">Срок окончания действия токена</param>
        /// <param name="claims">Объекты прав</param>
        /// <param name="signingCredentials">Объекты подписи</param>
        /// <returns></returns>
        private string CreateToken(string issuer, string audience, DateTime expires, Claim[] claims = null, SigningCredentials signingCredentials = null)
        {
            var securityToken = new JwtSecurityToken(
                claims: claims,
                issuer: issuer,
                audience: audience,
                expires: expires,
                signingCredentials: signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
        public string CreateAccessToken(Guid userId, string name, string email, string phone, string role)
        {
            var signingCredentials = new SigningCredentials(GetSymmetricSecurityKey(_jwtSettings.SecretKey), SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()),
                new Claim("ueid", userId.ToString()),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.MobilePhone, phone),
                new Claim(ClaimTypes.Role, role),
            };
            return CreateToken(_jwtSettings.Issuer, _jwtSettings.Audience, AccessTokenExpirationTime, claims, signingCredentials);
        }
        public string CreateRefreshToken(Guid userId)
        {
            var signingCredentials = new SigningCredentials(GetSymmetricSecurityKey(_jwtSettings.SecretKey), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Sid, Guid.NewGuid().ToString()),
                new Claim("ueid", userId.ToString()),
            };

            return CreateToken(_jwtSettings.Issuer, _jwtSettings.Audience, RefreshTokenExpirationTime, claims, signingCredentials);
        }
        public ClaimsPrincipal GetPrincipal(string token, out DateTime validTo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = CreateTokenValidationParameters(_jwtSettings.SecretKey, _jwtSettings.Issuer, _jwtSettings.Audience);

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            validTo = jwtSecurityToken.ValidTo;

            return principal;
        }
        public bool ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = CreateTokenValidationParameters(_jwtSettings.SecretKey, _jwtSettings.Issuer, _jwtSettings.Audience);
                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                return securityToken != null;
            }
            catch (SecurityTokenException)
            {
                return false;
            }
        }
    }
}

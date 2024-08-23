using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Infrastructure.Security;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace DELAY.Infrastructure.Helpers
{
    internal class PasswordHelper : IPasswordHelper
    {
        private readonly SecurityConfig _config;
        public PasswordHelper(IOptions<SecurityConfig> securityConfig)
        {
            _config = securityConfig.Value ?? throw new ArgumentNullException(nameof(SecurityConfig));
        }
        /// <summary>
        /// Создает ключ на основе простого текста
        /// </summary>
        /// <param name="plainText">Простой текст</param>
        /// <returns></returns>
        private byte[] CreateHashKey(string plainText)
        {
            if (string.IsNullOrWhiteSpace(plainText))
            {
                return [];
            }

            string hashAlgorithmName = !string.IsNullOrWhiteSpace(_config.HashAlgName) ?
                _config.HashAlgName.Trim().ToUpper() : "SHA512";

            return Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(plainText), Encoding.UTF8.GetBytes(_config.Salt),
                _config.Iterations ?? 1000, new HashAlgorithmName(hashAlgorithmName), _config.KeySize ?? 32);
        }

        public string GetHash(string plainText) => Convert.ToHexString(CreateHashKey(plainText));

        public bool IsEqual(string plainText, string hashText) => CryptographicOperations.FixedTimeEquals(CreateHashKey(plainText), Convert.FromHexString(hashText));
    }
}

using DELAY.Core.Application.Abstractions.Services.Base;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace DELAY.Infrastructure.Cryptography
{
    internal class CryptographyService : ICryptographyService
    {
        private readonly SecurityConfig _config;
        public CryptographyService(IOptions<SecurityConfig> securityConfig)
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
                return new byte[0];
            }
            string hashAlgorithmName = "SHA512";
            if (!string.IsNullOrWhiteSpace(_config.HashAlgName))
            {
                hashAlgorithmName = _config.HashAlgName.Trim().ToUpper();
            }
            return Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(plainText), Encoding.UTF8.GetBytes(_config.Salt),
                _config.Iterations ?? 1000, new HashAlgorithmName(hashAlgorithmName), _config.KeySize ?? 32);
        }

        public string GetHash(string plainText) => Convert.ToHexString(CreateHashKey(plainText));

        public bool IsEqual(string plainText, string hashText) => CryptographicOperations.FixedTimeEquals(CreateHashKey(plainText), Convert.FromHexString(hashText));
    }
}

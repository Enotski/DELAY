namespace DELAY.Infrastructure.Cryptography
{
    internal class SecurityConfig
    {
        /// <summary>
        /// Название раздела
        /// </summary>
        public const string SectionName = "SecurityConfig";

        public SecurityConfig()
        {
        }

        public SecurityConfig(string salt, string hashAlgName, int? iterations, int? keySize)
        {
            Salt = salt;
            HashAlgName = hashAlgName;
            Iterations = iterations;
            KeySize = keySize;
        }

        /// <summary>
        /// Соль
        /// </summary>
        public string Salt { get; set; }
        /// <summary>
        /// Алгоритм хэширования
        /// </summary>
        public string HashAlgName { get; init; }
        /// <summary>
        /// Число итераций
        /// </summary>
        public int? Iterations { get; init; }
        /// <summary>
        /// Длина выходного ключа
        /// </summary>
        public int? KeySize { get; init; }
    }
}

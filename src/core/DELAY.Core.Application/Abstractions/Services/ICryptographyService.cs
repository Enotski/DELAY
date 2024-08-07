namespace DELAY.Core.Application.Abstractions.Services.Base
{
    public interface ICryptographyService
    {
        /// <summary>
        /// Хэширование текста
        /// </summary>
        /// <param name="plainText">Простой текст</param>
        /// <returns></returns>
        string GetHash(string plainText);

        /// <summary>
        /// Сравнение текста
        /// </summary>
        /// <param name="plainText">Простой текст</param>
        /// <param name="hashText">Хэшированный текст</param>
        /// <returns></returns>
        bool IsEqual(string plainText, string hashText);
    }
}

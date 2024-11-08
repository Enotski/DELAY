namespace DELAY.Core.Application.Abstractions.Services.Auth
{
    /// <summary>
    /// Helper class of password utils
    /// </summary>
    public interface IPasswordHelper
    {
        /// <summary>
        /// Get hash from plain password
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        string GetHash(string plainText);

        /// <summary>
        /// Is euqal hashes
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="hashText"></param>
        /// <returns></returns>
        bool IsEqual(string plainText, string hashText);
    }
}

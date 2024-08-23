namespace DELAY.Core.Application.Abstractions.Services.Auth
{
    public interface IPasswordHelper
    {
        string GetHash(string plainText);

        bool IsEqual(string plainText, string hashText);
    }
}

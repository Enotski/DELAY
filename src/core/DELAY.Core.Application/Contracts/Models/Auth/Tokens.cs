namespace DELAY.Core.Application.Contracts.Models.Auth
{
    /// <summary>
    /// Tokens? generated after success auth
    /// </summary>
    public class Tokens
    {
        public Tokens()
        {
        }

        public Tokens(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

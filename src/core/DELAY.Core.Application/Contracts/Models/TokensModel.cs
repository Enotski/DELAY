namespace DELAY.Core.Application.Contracts.Models
{
    public class TokensModel
    {
        public TokensModel()
        {
        }

        public TokensModel(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

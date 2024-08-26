using DELAY.Core.Application.Contracts.Models.Base;

namespace DELAY.Presentation.RestAPI.Contracts.Response
{
    public class AuthResponseDto
    {
        public AuthResponseDto()
        {
        }

        public AuthResponseDto(TokensResponseDto tokens, IEnumerable<string> endpoints)
        {
            Endpoints = endpoints;
            Tokens = tokens;
        }
        public IEnumerable<string> Endpoints { get; set; }
        public TokensResponseDto Tokens { get; set; }
    }

    public class TokensResponseDto
    {
        public TokensResponseDto()
        {
        }

        public TokensResponseDto(string accessToken)
        {
            AccessToken = accessToken;
        }

        public string AccessToken { get; set; }
    }
}

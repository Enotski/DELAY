using DELAY.Core.Application.Contracts.Models.Base;

namespace DELAY.Presentation.RestAPI.Contracts.Response
{
    public class AuthResponseDto
    {
        public AuthResponseDto()
        {
        }

        public AuthResponseDto(TokensResponseDto tokens, IEnumerable<ApiEndpoint> endpoints)
        {
            Endpoints = endpoints;
            Tokens = tokens;
        }
        public IEnumerable<ApiEndpoint> Endpoints { get; set; }
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

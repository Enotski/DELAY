namespace DELAY.Presentation.RestAPI.Contracts
{
    public class AuthResponseDto
    {
        public AuthResponseDto()
        {
        }

        public AuthResponseDto(TokensResponseDto tokens, IEnumerable<ApiEndpointDto> endpoints)
        {
            Endpoints = endpoints;
            Tokens = tokens;
        }
        public IEnumerable<ApiEndpointDto> Endpoints { get; set; }
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

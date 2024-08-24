namespace DELAY.Core.Application.Contracts.Models.Auth
{
    public class GoogleAuthRequest : AuthUserAgentRequest
    {
        public GoogleAuthRequest()
        {
        }

        public string Code { get; set; }
    }
}

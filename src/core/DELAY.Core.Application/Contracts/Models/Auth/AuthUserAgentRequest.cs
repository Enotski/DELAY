namespace DELAY.Core.Application.Contracts.Models.Auth
{
    public abstract class AuthUserAgentRequest
    {
        public string? UserAgent { get; set; }

        public string? IpAddress { get; set; }

        public void SetUserAgentData(string userAgentData, string ip) {
            UserAgent = userAgentData;
            IpAddress = ip;
        }
    }
}

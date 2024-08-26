using System.ComponentModel.DataAnnotations;

namespace DELAY.Core.Application.Contracts.Models.Auth
{
    public class AuthUserAgentRequest
    {
        public AuthUserAgentRequest()
        {
        }

        public AuthUserAgentRequest(string fingerprint)
        {
            Fingerprint = fingerprint;
        }

        public AuthUserAgentRequest(string fingerprint, string? ipAddress, string? userAgent) : this(fingerprint)
        {
            IpAddress = ipAddress;
            UserAgent = userAgent;
        }

        [Required]
        public string Fingerprint { get; set; }

        public string? IpAddress { get; set; }

        public string? UserAgent { get; set; }

        public AuthUserAgentRequest SetUserAgentData(string ip, string userAgent)
        {
            IpAddress = ip;
            UserAgent = userAgent;

            return this;
        }
    }
}

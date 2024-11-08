using System.ComponentModel.DataAnnotations;

namespace DELAY.Core.Application.Contracts.Models.Auth
{
    /// <summary>
    /// User-agent
    /// </summary>
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

        /// <summary>
        /// Unique value
        /// </summary>
        [Required]
        public string Fingerprint { get; set; }

        /// <summary>
        /// User IP
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// User-agent info
        /// </summary>
        public string? UserAgent { get; set; }


        /// <summary>
        /// Set data
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public AuthUserAgentRequest SetUserAgentData(string ip, string userAgent)
        {
            IpAddress = ip;
            UserAgent = userAgent;

            return this;
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace DELAY.Core.Application.Contracts.Models.Auth
{
    /// <summary>
    /// Request to google openid
    /// </summary>
    public class GoogleAuthRequest : AuthUserAgentRequest
    {
        public GoogleAuthRequest()
        {
        }

        [Required]
        public string Code { get; set; }
    }
}

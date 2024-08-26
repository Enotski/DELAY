using System.ComponentModel.DataAnnotations;

namespace DELAY.Core.Application.Contracts.Models.Auth
{
    public class VkAuthRequest : AuthUserAgentRequest
    {
        public VkAuthRequest()
        {
        }

        [Required]
        public string Code { get; set; }

        [Required]
        public string DeviceId { get; set; }

        [Required]
        public string CodeVerifier { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace DELAY.Core.Application.Contracts.Models.Auth
{
    /// <summary>
    /// Request of auth
    /// </summary>
    public class SignInRequest : AuthUserAgentRequest
    {
        public SignInRequest()
        {
        }

        [Phone]
        public string? Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

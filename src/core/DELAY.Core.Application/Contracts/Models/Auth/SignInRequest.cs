using System.ComponentModel.DataAnnotations;

namespace DELAY.Core.Application.Contracts.Models.Auth
{
    public class SignInRequest
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

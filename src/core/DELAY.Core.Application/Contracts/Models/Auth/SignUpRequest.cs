using System.ComponentModel.DataAnnotations;

namespace DELAY.Core.Application.Contracts.Models.Auth
{
    public class SignUpRequest : SignInRequest
    {
        public SignUpRequest()
        {
        }

        [Required]
        public string Name { get; set; }
    }
}

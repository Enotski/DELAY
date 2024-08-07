using DELAY.Core.Application.Contracts.Models.Base;

namespace DELAY.Core.Application.Contracts.Models
{
    public class UserApiModel : KeyNameDto
    {
        public string Email { get; set; }

        public string PhoneNumber {  get; set; }

        public string Password { get; set; }
    }
}

using DELAY.Core.Application.Contracts.Models.Base;

namespace DELAY.Core.Application.Contracts.Models
{
    public class UserDto : KeyNameDto
    {
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }
    }
    public class UserCreateRequestDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

    }
}

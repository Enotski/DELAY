using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;

namespace DELAY.Core.Application.Contracts.Models.Dtos
{
    public class UserDto : KeyNameDto
    {
        public UserDto()
        {
        }

        public UserDto(Guid id, string? name, string? email, string? phoneNumber, string? password) : base(id, name)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
        }

        public string? Name { get; set; }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Password { get; set; }
    }

    public class EditCreateUserRequestDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

    }

    public class UserPasswordRequestDto : KeyDto
    {
        public string Password { get; set; }
    }
}

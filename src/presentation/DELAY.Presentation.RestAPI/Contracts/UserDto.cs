using DELAY.Core.Application.Contracts.Models.Base;

namespace DELAY.Core.Application.Contracts.Models
{
    public class UserDto : KeyDto
    {
        public UserDto()
        {
        }

        public UserDto(Guid id, string? name, string? email, string? phoneNumber, string? password) : base(id)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
        }

        public string? Name { get; set; }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Password { get; set; }
    }
    public class UserPasswordRequestDto: KeyDto
    {
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

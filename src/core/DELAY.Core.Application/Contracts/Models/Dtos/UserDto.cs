using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Domain.Enums;

namespace DELAY.Core.Application.Contracts.Models.Dtos
{
    public class UserDto
    {
        public UserDto()
        {
        }

        public UserDto(Guid id, string? name, string? email, string? phoneNumber, string? password)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Password = password;
        }

        public Guid? Id { get; set; }

        public string? Name { get; set; }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
        public RoleType Role { get; set; }
        public string? Password { get; set; }
    }

    public class UserPasswordRequestDto : KeyDto
    {
        public string Password { get; set; }
    }
}

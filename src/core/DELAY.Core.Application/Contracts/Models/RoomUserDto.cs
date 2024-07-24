using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Application.Contracts.Models
{
    public class RoomUserDto : IKey
    {
        public Guid Id { get; set; }
        public RoomDto Room { get; set; }
        public UserDto User { get; set; }
        public RoleType UserRole { get; set; }
    }
}

using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Interfaces;

namespace DELAY.Infrastructure.Persistence.Entities.Base
{
    public class RoomUserEntity : IKey
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public RoomEntity Room { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public RoleType UserRole { get; set; }
    }
}

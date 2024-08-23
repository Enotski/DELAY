using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Interfaces;
using DELAY.Infrastructure.Persistence.Entities.Base;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class RoomUserEntity : KeyModelEntity, IKey
    {
        public Guid RoomId { get; set; }
        public RoomEntity Room { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public RoleType UserRole { get; set; }
    }
}

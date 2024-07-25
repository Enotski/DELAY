using DELAY.Core.Domain.Enums;

namespace DELAY.Infrastructure.Persistence.Entities.Base
{
    public class BoardUserEntity : KeyModelEntity
    {
        public Guid BoardId { get; set; }
        public BoardEntity Board { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public RoleType UserRole { get; set; }
    }
}

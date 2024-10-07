using DELAY.Core.Domain.Enums;
using DELAY.Infrastructure.Persistence.Entities.Base;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class BoardUserEntity : KeyEntity
    {
        public Guid BoardId { get; set; }
        public BoardEntity Board { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        public BoardRoleType UserRole { get; set; }
    }
}

using DELAY.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class BoardUserEntity
    {
        public BoardUserEntity()
        {
        }

        public BoardUserEntity(Guid boardId, Guid userId, BoardRoleType userRole)
        {
            BoardId = boardId;
            UserId = userId;
            UserRole = userRole;
        }

        [Key]
        public Guid BoardId { get; set; }
        public BoardEntity Board { get; set; }

        [Key]
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        public BoardRoleType UserRole { get; set; }
    }
}

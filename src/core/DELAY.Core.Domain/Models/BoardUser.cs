using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class BoardUser
    {
        public BoardUser()
        {
        }

        public BoardUser(KeyNamedModel? boardId, KeyNamedModel user, BoardRoleType userRole)
        {
            BoardId = boardId;
            User = user;
            UserRole = userRole;
        }

        public KeyNamedModel? BoardId { get; set; }
        public KeyNamedModel User { get; set; }
        public BoardRoleType UserRole { get; set; }
    }
}

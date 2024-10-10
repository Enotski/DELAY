using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class BoardUser
    {
        public BoardUser()
        {
        }

        public BoardUser(KeyNamedModel board, KeyNamedModel user, BoardRoleType userRole)
        {
            Board = board;
            User = user;
            UserRole = userRole;
        }

        public KeyNamedModel Board { get; set; }
        public KeyNamedModel User { get; set; }
        public BoardRoleType UserRole { get; set; }
    }
}

using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class BoardUser : KeyModel
    {
        public BoardUser()
        {
        }

        public BoardUser(Board board, User user, RoleType userRole)
        {
            Board = board;
            User = user;
            UserRole = userRole;
        }

        public KeyNamedModel Board { get; set; }
        public KeyNamedModel User { get; set; }
        public RoleType UserRole { get; set; }
    }
}

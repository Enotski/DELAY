using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class BoardUser
    {
        public BoardUser()
        {
        }

        public BoardUser(KeyNamedModel user, BoardRoleType userRole)
        {
            User = user;
            UserRole = userRole;
        }

        public KeyNamedModel User { get; set; }
        public BoardRoleType UserRole { get; set; }
    }
}

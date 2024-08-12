using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Interfaces;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class RoomUser : KeyModel
    {
        public RoomUser()
        {
        }

        public KeyNamedModel Room { get; set; }
        public KeyNamedModel User { get; set; }
        public RoleType UserRole { get; set; }
    }
}

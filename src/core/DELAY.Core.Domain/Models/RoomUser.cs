using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Domain.Models
{
    public class RoomUser : IKey
    {
        public Guid Id { get; set; }
        public Room Room { get; set; }
        public User User { get; set; }
        public RoleType UserRole { get; set; }
    }
}

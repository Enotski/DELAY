using DELAY.Core.Domain.Enums;

namespace DELAY.Core.Domain.Models
{
    public class BoardUser
    {
        public Guid Id { get; set; }
        public Board Board { get; set; }
        public User User { get; set; }
        public RoleType UserRole { get; set; }
    }
}

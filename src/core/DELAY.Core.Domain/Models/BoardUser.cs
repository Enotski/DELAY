using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class BoardUser : KeyModel
    {
        public Board Board { get; set; }
        public User User { get; set; }
        public RoleType UserRole { get; set; }
    }
}

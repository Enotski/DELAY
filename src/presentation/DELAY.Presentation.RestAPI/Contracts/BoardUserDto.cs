using DELAY.Core.Domain.Enums;

namespace DELAY.Core.Application.Contracts.Models
{
    public class BoardUserDto
    {
        public Guid Id { get; set; }
        public BoardDto Board { get; set; }
        public UserApiModel User { get; set; }
        public RoleType UserRole { get; set; }
    }
}

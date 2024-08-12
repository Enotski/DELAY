using DELAY.Core.Application.Contracts.Models.Base;
using DELAY.Core.Domain.Enums;

namespace DELAY.Core.Application.Contracts.Models
{
    public class BoardUserDto
    {
        public Guid Id { get; set; }
        public KeyNameDto Board { get; set; }
        public KeyNameDto User { get; set; }
        public RoleType UserRole { get; set; }
    }
}

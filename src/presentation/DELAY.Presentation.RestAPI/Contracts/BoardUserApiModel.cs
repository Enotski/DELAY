using DELAY.Core.Application.Contracts.Models.Base;
using DELAY.Core.Domain.Enums;

namespace DELAY.Core.Application.Contracts.Models
{
    public class BoardUserApiModel
    {
        public Guid Id { get; set; }
        public KeyNameApiModel Board { get; set; }
        public KeyNameApiModel User { get; set; }
        public RoleType UserRole { get; set; }
    }
}

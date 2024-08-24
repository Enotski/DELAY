using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Interfaces;
using DELAY.Infrastructure.Persistence.Entities.Base;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class UserEntity : KeyModelEntity, IName
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Password { get; set; }

        public DateTime ChangedDate { get; set; }

        public DateTime CreateDate { get; set; }

        public string? ChangedBy { get; set; }

        public RoleType Role { get; set; }

        public ICollection<TicketEntity> ChangedTickets { get; set; }

        public ICollection<SessionLogEntity> Sessions { get; set; }

        public ICollection<TicketUserEntity> AssignedTickets { get; set; } = new List<TicketUserEntity>();

        public ICollection<BoardUserEntity> BoardUsers { get; set; } = new List<BoardUserEntity>();

        public ICollection<RoomUserEntity> RoomUsers { get; set; } = new List<RoomUserEntity>();
    }
}

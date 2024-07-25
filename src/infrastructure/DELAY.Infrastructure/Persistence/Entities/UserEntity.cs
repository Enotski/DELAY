namespace DELAY.Infrastructure.Persistence.Entities.Base
{
    public class UserEntity : KeyNamedModelEntity
    {
        public ICollection<TicketEntity> ChangedTickets { get; set; }

        public ICollection<TicketUserEntity> AssignedTickets { get; set; } = new List<TicketUserEntity>();

        public ICollection<BoardUserEntity> BoardUsers { get; set; } = new List<BoardUserEntity>();

        public ICollection<RoomUserEntity> RoomUsers { get; set; } = new List<RoomUserEntity>();
    }
}

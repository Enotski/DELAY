using DELAY.Core.Domain.Interfaces;
using DELAY.Infrastructure.Persistence.Entities.Base;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class BoardEntity : KeyNamedModelEntity, IDescriptioin
    {
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string ChangedBy { get; set; }
        public DateTime ChangeDate { get; set; }
        public DateTime CreateDate { get; set; }

        public Guid? RoomId { get; set; }
        public RoomEntity Room { get; set; }
        public ICollection<BoardUserEntity> BoardUsers { get; set; } = new List<BoardUserEntity>();
    }
}

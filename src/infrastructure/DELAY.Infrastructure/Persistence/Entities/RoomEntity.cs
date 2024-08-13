using DELAY.Core.Domain.Interfaces;

namespace DELAY.Infrastructure.Persistence.Entities.Base
{
    public class RoomEntity : KeyNamedModelEntity, IDescriptioin
    {
        public ICollection<RoomUserEntity> RoomUsers { get; set; } = new List<RoomUserEntity>();
        public ICollection<BoardEntity> Boards { get; set; } = new List<BoardEntity>();
        public string Description { get; set; }
    }
}

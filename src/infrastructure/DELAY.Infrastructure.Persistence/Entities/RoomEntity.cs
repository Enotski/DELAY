using DELAY.Core.Domain.Interfaces;
using DELAY.Infrastructure.Persistence.Entities.Base;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class RoomEntity : KeyNamedModelEntity, IDescriptioin
    {
        public ICollection<RoomUserEntity> RoomUsers { get; set; } = new List<RoomUserEntity>();
        public ICollection<BoardEntity> Boards { get; set; } = new List<BoardEntity>();
        public string Description { get; set; }
    }
}

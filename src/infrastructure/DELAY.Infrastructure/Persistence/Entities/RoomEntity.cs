using DELAY.Core.Domain.Interfaces;

namespace DELAY.Infrastructure.Persistence.Entities.Base
{
    public class RoomEntity : KeyNamedModelEntity, IDescriptioin
    {
        public ICollection<RoomUserEntity> RoomUsers { get; set; } = new List<RoomUserEntity>();
        public string Description { get; set; }
    }
}

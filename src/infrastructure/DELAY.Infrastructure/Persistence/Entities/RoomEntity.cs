namespace DELAY.Infrastructure.Persistence.Entities.Base
{
    public class RoomEntity : KeyNamedModelEntity
    {
        public ICollection<RoomUserEntity> RoomUsers { get; set; } = new List<RoomUserEntity>();
    }
}

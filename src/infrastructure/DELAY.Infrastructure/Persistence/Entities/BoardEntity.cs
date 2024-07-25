using DELAY.Core.Domain.Interfaces;

namespace DELAY.Infrastructure.Persistence.Entities.Base
{
    public class BoardEntity : KeyNamedModelEntity, IDescriptioin
    {
        public string Description { get; set; }
        public ICollection<BoardUserEntity> BoardUsers { get; set; } = new List<BoardUserEntity>();
    }
}

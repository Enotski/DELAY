namespace DELAY.Infrastructure.Persistence.Entities.Base
{
    public class BoardEntity : KeyNamedModelEntity
    {
        public string Description { get; set; }
        public ICollection<BoardUserEntity> BoardUsers { get; set; } = new List<BoardUserEntity>();
    }
}

using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class Board : KeyNamedModel
    {
        public string Description { get; set; }
        public ICollection<BoardUser> BoardUsers { get; set; } = new List<BoardUser>();
    }
}

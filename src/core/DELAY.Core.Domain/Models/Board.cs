using DELAY.Core.Domain.Interfaces;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class Board : KeyNamedModel, IDescriptioin
    {
        public Board(string name, string description, IEnumerable<BoardUser> boardUsers) : base(name)
        {
            Description = description;
            BoardUsers = boardUsers;
        }

        public Board(Guid id, string name, string description, IEnumerable<BoardUser> boardUsers) : base(id, name)
        {
            Description = description;
            BoardUsers = boardUsers;
        }

        public string Description { get; set; }
        public IEnumerable<BoardUser> BoardUsers { get; set; } = new List<BoardUser>();
    }
}

using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Domain.Enums;

namespace DELAY.Core.Application.Contracts.Models.ModelSelectors
{
    public class BoardSelector : KeyNameDto
    {
        public BoardSelector(Guid id, string name, string description, IEnumerable<BoardUserSelector> users) : base(id, name)
        {
            Description = description;
            Users = users;
        }

        public string Description { get; set; }

        public IEnumerable<BoardUserSelector> Users { get; set; }
    }

    public class BoardUserSelector
    {
        public BoardUserSelector(KeyNameSelector board, KeyNameSelector user, BoardRoleType userRole)
        {
            Board = board;
            User = user;
            UserRole = userRole;
        }

        public KeyNameSelector Board { get; set; }
        public KeyNameSelector User { get; set; }
        public BoardRoleType UserRole { get; set; }
    }
}

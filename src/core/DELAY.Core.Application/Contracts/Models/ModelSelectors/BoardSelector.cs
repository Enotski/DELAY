using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Domain.Enums;

namespace DELAY.Core.Application.Contracts.Models.ModelSelectors
{
    public class BoardSelector : KeyNameDto
    {
        public BoardSelector(Guid id, string name, string description, bool isPublic, IEnumerable<BoardUserSelector> users) : base(id, name)
        {
            Description = description;
            Users = users;
            IsPublic = isPublic;
        }

        public string Description { get; set; }
        public bool IsPublic { get; set; }

        public IEnumerable<BoardUserSelector> Users { get; set; }
    }

    public class BoardUserSelector
    {
        public BoardUserSelector(KeyNameSelector user, BoardRoleType userRole)
        {
            User = user;
            UserRole = userRole;
        }

        public KeyNameSelector User { get; set; }
        public BoardRoleType UserRole { get; set; }
    }
}

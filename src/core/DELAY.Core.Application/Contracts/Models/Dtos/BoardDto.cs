using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Domain.Enums;

namespace DELAY.Core.Application.Contracts.Models.Dtos
{
    public class BoardDto : KeyNameDto
    {
        public bool IsPublic { get; set; }
        public string Description { get; set; }

        public IList<BoardUserDto> Users { get; set; }
    }

    public class BoardUserDto
    {
        public KeyNameDto User { get; set; }
        public BoardRoleType UserRole { get; set; }
    }
}

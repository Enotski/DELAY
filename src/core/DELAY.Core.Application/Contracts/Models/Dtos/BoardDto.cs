using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Domain.Enums;

namespace DELAY.Core.Application.Contracts.Models.Dtos
{
    public class BoardDto : KeyNameDto
    {
        public string Description { get; set; }

        public IEnumerable<BoardUserDto> Users { get; set; }
    }

    public class BoardUserDto
    {
        public KeyNameDto Board { get; set; }
        public KeyNameDto User { get; set; }
        public RoleType UserRole { get; set; }
    }

    public class EditBoardRequestDto : KeyNameDto
    {
        public string Description { get; set; }

    }
}

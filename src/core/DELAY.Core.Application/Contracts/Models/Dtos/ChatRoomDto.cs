using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Application.Contracts.Models.ModelSelectors.Base;
using DELAY.Core.Domain.Enums;

namespace DELAY.Core.Application.Contracts.Models.Dtos
{
    public class ChatRoomDto : KeyNameDto
    {
        public IEnumerable<Guid> Boards { get; set; }

        public IEnumerable<ChatRoomUserDto> Users { get; set; }
    }

    public class ChatRoomUserDto : KeyNameDto
    {
        public ChatRoomRoleType Role { get; set; }
    }
}

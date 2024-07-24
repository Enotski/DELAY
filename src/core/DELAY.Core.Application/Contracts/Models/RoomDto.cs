using DELAY.Core.Application.Contracts.Models.Base;

namespace DELAY.Core.Application.Contracts.Models
{
    public class RoomDto : KeyNameDto
    {
        public IEnumerable<RoomUserDto> RoomUsers { get; set; }
    }
}

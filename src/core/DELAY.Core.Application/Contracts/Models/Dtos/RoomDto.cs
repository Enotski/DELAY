using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Core.Domain.Enums;

namespace DELAY.Core.Application.Contracts.Models.Dtos
{
    public class RoomDto
    {
        public RoomDto()
        {
        }

        public RoomDto(Guid? id, string name, bool isPublic, RoomType chatType, IEnumerable<KeyNameDto> boards, IEnumerable<RoomUserDto> users)
        {
            Id = id;
            Name = name;
            IsPublic = isPublic;
            ChatType = chatType;
            Boards = boards;
            Users = users;
        }

        public Guid? Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public RoomType ChatType { get; set; }


        public IEnumerable<KeyNameDto> Boards { get; set; }
        public IEnumerable<RoomUserDto> Users { get; set; }
    }

    public class RoomUserDto
    {
        public RoomUserDto()
        {
        }

        public RoomUserDto(KeyNameDto user, ChatRoomRoleType role)
        {
            User = user;
            Role = role;
        }

        public KeyNameDto User { get; set; }
        public ChatRoomRoleType Role { get; set; }
    }
}

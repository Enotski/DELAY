using DELAY.Core.Domain.Interfaces;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class Room : KeyNamedModel, IDescriptioin
    {
        public Room()
        {
        }

        public Room(string name, string description, IEnumerable<RoomUser> roomUsers) : base(name)
        {
            Description = description;
            RoomUsers = roomUsers;
        }

        public Room(Guid id, string name, string description, IEnumerable<RoomUser> roomUsers) : base(id, name)
        {
            Description = description;
            RoomUsers = roomUsers;
        }

        public IEnumerable<Board> Boards { get; set; }
        public IEnumerable<RoomUser> RoomUsers { get; set; }

        public string Description { get; set; }
    }
}

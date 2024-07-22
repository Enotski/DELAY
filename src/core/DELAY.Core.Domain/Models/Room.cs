using DELAY.Core.Domain.Interfaces;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class Room : KeyNamedModel
    {
        public IEnumerable<RoomUser> RoomUsers { get; set; }
    }
}

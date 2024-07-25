using DELAY.Core.Domain.Interfaces;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class Room : KeyNamedModel, IDescriptioin
    {
        public IEnumerable<RoomUser> RoomUsers { get; set; }

        public string Description { get; set; }
    }
}

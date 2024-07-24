using DELAY.Core.Application.Contracts.Models.Base;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Application.Contracts.Models
{
    public class BoardDto : KeyNameDto
    {
        public string Description { get; set; }
        public ICollection<BoardUserDto> BoardUsers { get; set; } = new List<BoardUserDto>();
    }
}

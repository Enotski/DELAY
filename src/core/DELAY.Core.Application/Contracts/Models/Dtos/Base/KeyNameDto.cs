using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Application.Contracts.Models.Dtos.Base
{
    public class KeyNameDto : KeyDto, IName
    {
        public KeyNameDto() : base()
        { }

        public KeyNameDto(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}

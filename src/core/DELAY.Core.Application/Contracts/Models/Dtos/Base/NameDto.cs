using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Application.Contracts.Models.Dtos.Base
{
    public class NameDto : IName
    {
        public NameDto() : base()
        { }

        public NameDto(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}

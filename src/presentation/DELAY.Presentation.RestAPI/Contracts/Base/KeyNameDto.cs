using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Application.Contracts.Models.Base
{
    public class KeyNameDto : KeyDto, IName
    {
        public KeyNameDto() : base()
        { }

        public string Name { get; set; }
    }
}

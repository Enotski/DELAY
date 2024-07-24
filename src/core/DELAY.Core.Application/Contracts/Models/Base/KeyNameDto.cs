using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Application.Contracts.Models.Base
{
    public class KeyNameDto : BaseDto, IName
    {
        public KeyNameDto() : base()
        { }

        public string Name { get; set; }
    }
}

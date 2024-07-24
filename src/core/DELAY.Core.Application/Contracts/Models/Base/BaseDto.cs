using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Application.Contracts.Models.Base
{
    public class BaseDto : IKey
    {
        public BaseDto() { }

        public Guid Id { get; set; }
    }
}

using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Application.Contracts.Models.Base
{
    public class KeyDto : IKey
    {
        public KeyDto() { }

        public Guid Id { get; set; }
    }
}

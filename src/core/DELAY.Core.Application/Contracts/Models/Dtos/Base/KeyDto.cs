using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Application.Contracts.Models.Dtos.Base
{
    /// <summary>
    /// Keyied model
    /// </summary>
    public class KeyDto : IKey
    {
        public KeyDto() { }

        public KeyDto(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}

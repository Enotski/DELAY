using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Application.Contracts.Models.Base
{
    public class KeyApiModel : IKey
    {
        public KeyApiModel() { }

        public Guid Id { get; set; }
    }
}

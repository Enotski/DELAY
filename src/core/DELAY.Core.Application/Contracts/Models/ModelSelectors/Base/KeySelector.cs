using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Application.Contracts.Models.ModelSelectors.Base
{
    public class KeySelector : IKey
    {
        public KeySelector() { }

        public KeySelector(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}

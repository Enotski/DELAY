using DELAY.Core.Domain.Interfaces;

namespace DELAY.Infrastructure.Persistence.Entities.Base
{
    public abstract class KeyEntity : IKey
    {
        public Guid Id { get; set; }
    }
}

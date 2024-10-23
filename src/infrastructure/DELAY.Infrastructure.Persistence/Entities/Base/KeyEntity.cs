using DELAY.Core.Domain.Interfaces;

namespace DELAY.Infrastructure.Persistence.Entities.Base
{
    public abstract class KeyEntity : IKey
    {
        protected KeyEntity()
        {
        }

        public Guid Id { get; set; }
    }
}

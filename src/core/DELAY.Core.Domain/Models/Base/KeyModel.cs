using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Domain.Models.Base
{
    public abstract class KeyModel : IKey
    {
        public Guid Id { get; set; }
    }
}

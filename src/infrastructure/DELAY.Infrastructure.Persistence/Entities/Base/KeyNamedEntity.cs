using DELAY.Core.Domain.Interfaces;

namespace DELAY.Infrastructure.Persistence.Entities.Base
{
    public class KeyNamedEntity : KeyEntity, IName
    {
        public string Name { get; set; }
    }
}

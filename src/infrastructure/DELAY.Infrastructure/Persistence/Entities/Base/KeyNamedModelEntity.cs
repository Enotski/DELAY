using DELAY.Core.Domain.Interfaces;

namespace DELAY.Infrastructure.Persistence.Entities.Base
{
    public class KeyNamedModelEntity : KeyModelEntity, IName
    {
        public string Name { get; set; }
    }
}

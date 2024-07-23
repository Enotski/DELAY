using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Domain.Models.Base
{
    public class KeyNamedModel : KeyModel, IName
    {
        public string Name { get; set; }
    }
}

using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Application.Contracts.Models.Base
{
    public class KeyNameApiModel : KeyApiModel, IName
    {
        public KeyNameApiModel() : base()
        { }

        public string Name { get; set; }
    }
}

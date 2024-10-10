using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Application.Contracts.Models.ModelSelectors.Base
{
    public class KeyNameSelector : KeySelector, IName
    {
        public KeyNameSelector() : base()
        { }

        public KeyNameSelector(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}

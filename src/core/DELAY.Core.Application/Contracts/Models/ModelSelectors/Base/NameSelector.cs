using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Application.Contracts.Models.ModelSelectors.Base
{
    public class NameSelector : IName
    {
        public NameSelector() : base()
        { }

        public NameSelector(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}

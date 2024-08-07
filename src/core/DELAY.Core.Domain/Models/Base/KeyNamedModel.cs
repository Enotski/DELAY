using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Domain.Models.Base
{
    public class KeyNamedModel : KeyModel, IName
    {
        public KeyNamedModel(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public KeyNamedModel(string name) : base()
        {
            Name = name;
        }

        public string Name
        {
            get => name; 
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Name));

                name = value;
            }
        }
        private string name;

        public void Update(string name)
        {
            Name = name;
        }
    }
}

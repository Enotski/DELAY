using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Domain.Models.Base
{
    public abstract class KeyNamedModel : KeyModel, IName
    {
        protected KeyNamedModel(Guid id, string name) : base(id)
        {
            Name = name;
        }

        protected KeyNamedModel(string name) : base()
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

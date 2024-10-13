using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Domain.Models.Base
{
    public class KeyNamedModel : KeyModel, IName
    {
        public KeyNamedModel(Guid id, string name) : base(id)
        {
            Name = name;
        }
        public KeyNamedModel() : base()
        {
        }
        public KeyNamedModel(string name) : base()
        {
            Name = name;
        }
        public KeyNamedModel(KeyNamedModel model) : base()
        {
            Id = model.Id;
            Name = model.Name;
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


        public bool IsValidName()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }

        public void Update(string name)
        {
            Name = name;
        }
    }
}

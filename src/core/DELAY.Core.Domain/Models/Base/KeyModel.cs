using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Domain.Models.Base
{
    public abstract class KeyModel : IKey
    {
        protected KeyModel()
        {
            Id = Guid.NewGuid();
        }

        protected KeyModel(Guid id)
        {
            Id = id;
        }

        public Guid Id
        {
            get => id;
            set {
                if (value == Guid.Empty)
                    throw new ArgumentException(nameof(Id));

                Id = value;
            }
        }
        private readonly Guid id;
    }
}

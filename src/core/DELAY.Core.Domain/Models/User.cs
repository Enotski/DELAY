using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class User : KeyNamedModel
    {
        public User(string name) : base(name)
        {
        }
        public User(Guid id, string name) : base(id, name)
        {
        }
    }
}

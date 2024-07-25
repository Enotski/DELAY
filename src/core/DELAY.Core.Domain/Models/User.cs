using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class User : KeyNamedModel
    {
        public User(string name) : base(name)
        {
            ChangedDate = DateTime.UtcNow;
        }

        public User(Guid id, string name) : base(id, name)
        {
        }

        public User(Guid id, string name, DateTime changedDate) : base(id, name)
        {
            ChangedDate = changedDate;
        }

        public DateTime ChangedDate { get; set; }
    }
}

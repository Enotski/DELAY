using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Application.Contracts.Models
{
    public class OperationUserInfo : KeyNamedModel
    {
        public OperationUserInfo(Guid id, string name, string email) : base(id, name)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}

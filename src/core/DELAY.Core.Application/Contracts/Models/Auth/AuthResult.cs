using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Application.Contracts.Models.Auth
{
    public class AuthResult : KeyNamedModel
    {
        public AuthResult(Guid id, string name, RoleType role, Tokens tokens)
        {
            Role = role;
            Tokens = tokens;
        }

        public RoleType Role { get; set; }
        public Tokens Tokens { get; set; }
    }
}

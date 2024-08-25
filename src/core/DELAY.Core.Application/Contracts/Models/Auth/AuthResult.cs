using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Interfaces;

namespace DELAY.Core.Application.Contracts.Models.Auth
{
    public class AuthResult : IName
    {
        public AuthResult(string email, string phone, string name, RoleType role, Tokens tokens)
        {
            Email = email;
            Name = name;
            Role = role;
            Tokens = tokens;
            Phone = phone;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public RoleType Role { get; set; }
        public Tokens Tokens { get; set; }
    }
}

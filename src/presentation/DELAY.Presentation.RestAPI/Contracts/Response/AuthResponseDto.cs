using DELAY.Core.Application.Contracts.Models.Base;

namespace DELAY.Presentation.RestAPI.Contracts.Response
{
    public class AuthResponseDto
    {
        public AuthResponseDto()
        {
        }

        public AuthResponseDto(string email, string phone, string name, IEnumerable<string> endpoints)
        {
            Email = email;
            Phone = phone;
            Name = name;
            Endpoints = endpoints;
        }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public IEnumerable<string> Endpoints { get; set; }
    }
}

using DELAY.Core.Application.Contracts.Models.Base;

namespace DELAY.Presentation.RestAPI.Contracts.Response
{
    public class AuthResponseDto : KeyNameDto
    {
        public AuthResponseDto()
        {
        }

        public AuthResponseDto(Guid id, string name, IEnumerable<string> endpoints) : base(id, name)
        {
            Endpoints = endpoints;
        }

        public IEnumerable<string> Endpoints { get; set; }
    }
}

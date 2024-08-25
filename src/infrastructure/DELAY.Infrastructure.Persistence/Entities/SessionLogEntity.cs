using DELAY.Core.Domain.Enums;
using DELAY.Infrastructure.Persistence.Entities.Base;

namespace DELAY.Infrastructure.Persistence.Entities
{
    public class SessionLogEntity : KeyModelEntity
    {
        public SessionLogEntity()
        {
        }

        public AuthProviderType AuthProvider { get; set; }

        public DateTime StartTime { get; set; }

        public Guid UserId { get; set; }

        public string IpAddress {  get; set; }

        public string UserAgent { get; set; }

        public UserEntity User { get; set; }
    }
}

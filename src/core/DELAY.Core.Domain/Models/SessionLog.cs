using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class SessionLog : KeyModel
    {
        public SessionLog()
        {
        }

        public SessionLog(Guid userId, string userAgent, string ipAdress, DateTime startTime, AuthProviderType authProvider)
        {
            UserId = userId;
            UserAgent = userAgent;
            IpAddress = ipAdress;
            StartTime = startTime;
            AuthProvider = authProvider;
        }

        public Guid UserId { get; set; }

        public string UserAgent { get; set; }

        public string IpAddress { get; set; }

        public DateTime StartTime { get; set; }

        public AuthProviderType AuthProvider { get; set; }

        public void Update(AuthProviderType authProvider)
        {
            AuthProvider = authProvider;
            StartTime = DateTime.UtcNow;
        }
    }
}

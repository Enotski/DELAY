using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Domain.Models
{
    public class SessionLog : KeyModel
    {
        public SessionLog(Guid userId, string userAgent, string ip, DateTime startTime, AuthProviderType authProvider)
        {
            UserId = userId;
            UserAgent = userAgent;
            Ip = ip;
            StartTime = startTime;
            AuthProvider = authProvider;
        }

        public Guid UserId { get; set; }

        public string UserAgent { get; set; }

        public string Ip { get; set; }

        public DateTime StartTime { get; set; }

        public AuthProviderType AuthProvider { get; set; }

        public void Update()
        {
            StartTime = DateTime.UtcNow;
        }
    }
}

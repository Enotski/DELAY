using DELAY.Core.Domain.Enums;
using DELAY.Core.Domain.Models.Base;

namespace DELAY.Core.Application.Contracts.Models
{
    internal class SessionCache : KeyModel
    {
        public SessionCache()
        {
        }

        public SessionCache(Guid id, string refreshToken, Guid userId, string? fingerprint, string? ipAddress, string? userAgent, DateTime createdTime, uint expiresInDays, AuthProviderType authProvider) : base(id)
        {
            RefreshToken = refreshToken;
            UserId = userId;
            Fingerprint = fingerprint;
            IpAddress = ipAddress;
            UserAgent = userAgent;
            CreatedTime = createdTime;
            ExpiresInDays = expiresInDays;
            AuthProvider = authProvider;
        }

        public string RefreshToken { get; set; }
        public Guid UserId { get; set; }
        public string? Fingerprint { get; set; }
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }
        public DateTime CreatedTime { get; set; }
        public uint ExpiresInDays { get; set; }
        public AuthProviderType AuthProvider {  get; set; }
    }

    internal class RootUserSessionCache
    {
        public RootUserSessionCache()
        {
        }

        public RootUserSessionCache(IEnumerable<string> sessionsKeys)
        {
            SessionsKeys = sessionsKeys;
        }
        public IEnumerable<string> SessionsKeys { get; set; }
    }
}

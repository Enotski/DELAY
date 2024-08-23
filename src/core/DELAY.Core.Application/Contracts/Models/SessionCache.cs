namespace DELAY.Core.Application.Contracts.Models
{
    internal class SessionCache
    {
        public SessionCache()
        {
        }

        public SessionCache(string refreshToken, Guid sessionId, Guid userId)
        {
            RefreshToken = refreshToken;
            SessionId = sessionId;
            UserId = userId;
        }

        public string RefreshToken { get; set; }
        public Guid SessionId { get; set; }
        public Guid UserId { get; set; }
    }
}

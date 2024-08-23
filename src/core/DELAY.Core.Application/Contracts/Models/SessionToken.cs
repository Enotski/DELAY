namespace DELAY.Core.Application.Contracts.Models
{
    internal class SessionToken
    {
        public SessionToken(Guid sessionId, string refreshToken)
        {
            SessionId = sessionId;
            RefreshToken = refreshToken;
        }

        public Guid SessionId { get; set; }
        public string RefreshToken { get; set; }
    }
}

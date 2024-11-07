using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace DELAY.Presentation.RestAPI.Hubs
{
    public interface INotificationClient
    {
        Task Notify(string message);
    }

    [Authorize(AuthenticationSchemes = "Jwt")]
    public class NotificationHub : Hub<INotificationClient>
    {
        protected string ClientId => Context.User.FindFirst(x => x.Type == "ueid")?.Value;

        protected string ConnectionId => Context.ConnectionId;


        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
        public string GetUserIdentifier()
        {
            return Context.UserIdentifier;
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(ConnectionId, ClientId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(ConnectionId, ClientId);
        }
    }

    public class CustomUserIdProvider : IUserIdProvider
    {
        public virtual string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(x => x.Type == "ueid")?.Value;
        }
    }
}

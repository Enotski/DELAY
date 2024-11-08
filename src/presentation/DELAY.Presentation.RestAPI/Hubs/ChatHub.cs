using DELAY.Core.Application.Abstractions.Services.Boards;
using DELAY.Core.Application.Abstractions.Services.Rooms;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace DELAY.Presentation.RestAPI.Hubs
{
    [Authorize(AuthenticationSchemes = "Jwt")]
    public class ChatHub : Hub
    {
        /// <summary>
        /// <inheritdoc cref="IBoardService"/>
        /// </summary>
        private readonly IChatRoomService chatService;


        public ChatHub(IChatRoomService chatService)
        {
            this.chatService = chatService;
        }

        protected OperationUserInfo TryGetUser()
        {
            var id = Context.User.FindFirst(x => x.Type == "ueid").Value;
            var name = Context.User.FindFirst(x => x.Type == ClaimTypes.Name).Value;
            var email = Context.User.FindFirst(x => x.Type == ClaimTypes.Email).Value;

            return new OperationUserInfo(Guid.Parse(id), name, email);
        }

        [HubMethodName("PostMessage")]
        public async Task PostMessage(ChatMessageDto model)
        {
            try
            {
                var user = TryGetUser();

                var res = await chatService.CreateMessageAsync(model, user);

                await Clients.Group(model.ChatId.ToString()).SendAsync("NewMessage", res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

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
            var chats = await chatService.GetByUserAsync(Guid.Parse(ClientId));

            await Task.WhenAll(chats.Select(x => Groups.AddToGroupAsync(ConnectionId, x.Id.ToString())));
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var chats = await chatService.GetByUserAsync(Guid.Parse(ClientId));

            await Task.WhenAll(chats.Select(x => Groups.RemoveFromGroupAsync(ConnectionId, x.Id.ToString())));
        }
    }
}

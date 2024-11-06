using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Abstractions.Services.Boards;
using DELAY.Core.Application.Abstractions.Services.Common;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Dtos;
using DELAY.Presentation.RestAPI.Controllers.Base;
using DELAY.Presentation.RestAPI.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DELAY.Core.Application.Abstractions.Services
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketController : AuthorizedController
    {
        /// <summary>
        /// <inheritdoc cref="IBoardService"/>
        /// </summary>
        private readonly IBoardService boardService;

        /// <summary>
        /// <inheritdoc cref="ILogger"/>
        /// </summary>
        private readonly ILogger<TicketController> logger;

        /// <summary>
        /// <inheritdoc cref="IModelMapperService"/>
        /// </summary>
        private readonly IModelMapperService mapper;

        /// <summary>
        /// <inheritdoc cref="NotificationHub"/>
        /// </summary>
        private readonly IHubContext<NotificationHub, INotificationClient> notificationHub;

        public TicketController(IBoardService userService, IModelMapperService mapper, ITokensService tokensService, ILogger<TicketController> logger, IHubContext<NotificationHub, INotificationClient> notificationHub) : base(tokensService)
        {
            this.logger = logger;

            this.boardService = userService ?? throw new ArgumentNullException(nameof(IBoardService));

            this.mapper = mapper ?? throw new ArgumentNullException(nameof(IModelMapperService));

            this.notificationHub = notificationHub ?? throw new ArgumentNullException(nameof(IHubContext<NotificationHub>));
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] Guid id, [FromQuery] Guid boardId)
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var model = await boardService.GetTicketAsync(new TicketRequestDto() { Id = id, BoardId = boardId}, user);

                if (model == null)
                {
                    return Problem("Not found by key", statusCode: StatusCodes.Status404NotFound);
                }

                return Ok(model);
            }
            catch (Exception exp)
            {
                return HandleError(logger, exp, "Get board by key error", code: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet, Route("by-list")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByChatAsync([FromQuery] Guid listId, [FromQuery] Guid boardId)
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var model = await boardService.GetTicketsByListAsync(new TicketsByListRequestDto() { ListId = listId, BoardId = boardId}, user);

                if (model == null)
                {
                    return Problem("Not found by key", statusCode: StatusCodes.Status404NotFound);
                }

                return Ok(model);
            }
            catch (Exception exp)
            {
                return HandleError(logger, exp, "Get board by key error", code: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet, Route("by-user")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByUserAsync()
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var model = await boardService.GetTicketsByUserAsync(user);

                if (model == null)
                {
                    return Problem("Not found by user", statusCode: StatusCodes.Status404NotFound);
                }

                return Ok(model);
            }
            catch (Exception exp)
            {
                return HandleError(logger, exp, "Create board by user error", code: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTicketAsync([FromBody] TicketDto model)
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var result = await boardService.CreateTicketAsync(model, user);
                               
                await notificationHub.Clients.Users(result.NewTicketUsers.Select(x => x.ToString())).Notify($"Ticket {result.Id} was added");

                return Created(result.ToString(), result);
            }
            catch (Exception exp)
            {
                return HandleError(logger, exp, "Create board error", code: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync([FromBody] TicketDto model)
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var result = await boardService.UpdateTicketAsync(model, user);

                await notificationHub.Clients.Users(result.TicketUsers.Select(x => x.ToString())).Notify($"Ticket {result.Id} was updated");
                await notificationHub.Clients.Users(result.RemovedTicketUsers.Select(x => x.ToString())).Notify($"You was un-assigned from ticket {result.Id}");
                await notificationHub.Clients.Users(result.NewTicketUsers.Select(x => x.ToString())).Notify($"You was assigned to ticket {result.Id}");

                return Ok(result);
            }
            catch (Exception exp)
            {
                return HandleError(logger, exp, "Create board error", code: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromBody] TicketRequestDto model)
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var result = await boardService.DeleteTicketAsync(model, user);

                await notificationHub.Clients.Users(result.RemovedTicketUsers.Select(x => x.ToString())).Notify($"Ticket {result.Id} was removed");

                return Ok(result);
            }
            catch (Exception exp)
            {
                return HandleError(logger, exp, "Create board error", code: StatusCodes.Status500InternalServerError);
            }
        }
    }
}

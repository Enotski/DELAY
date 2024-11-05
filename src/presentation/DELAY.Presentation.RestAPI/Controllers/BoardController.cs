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
    [Route("api/boards")]
    public class BoardController : AuthorizedController
    {
        /// <summary>
        /// <inheritdoc cref="IBoardService"/>
        /// </summary>
        private readonly IBoardService boardService;

        /// <summary>
        /// <inheritdoc cref="ILogger"/>
        /// </summary>
        private readonly ILogger<BoardController> logger;

        /// <summary>
        /// <inheritdoc cref="IModelMapperService"/>
        /// </summary>
        private readonly IModelMapperService mapper;

        /// <summary>
        /// <inheritdoc cref="NotificationHub"/>
        /// </summary>
        private readonly IHubContext<NotificationHub, INotificationClient> notificationHub;

        public BoardController(IBoardService userService, IModelMapperService mapper, ITokensService tokensService, ILogger<BoardController> logger, IHubContext<NotificationHub, INotificationClient> notificationHub) : base(tokensService)
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
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                await notificationHub.Clients.User(user.Id.ToString()).Notify("GetAsync");

                var model = await boardService.GetBoardAsync(id, user);

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

        [HttpGet, Route("by-chat")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByChatAsync(Guid chatId)
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var model = await boardService.GetBoardByChatAsync(chatId, user);

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
                notificationHub.Clients.All.Notify("Hello");

                TryGetUser(out OperationUserInfo user);

                var model = await boardService.GetBoardByUserAsync(user.Id);

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBoardAsync([FromBody] BoardDto model)
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var result = await boardService.CreateBoardAsync(model, user);

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
        public async Task<IActionResult> UpdateBoardAsync([FromBody] BoardDto model)
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var result = await boardService.UpdateBoardAsync(model, user);

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
        public async Task<IActionResult> DeleteBoardAsync([FromBody] Guid id)
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var result = await boardService.DeleteAsync(id, user);

                return Ok(result);
            }
            catch (Exception exp)
            {
                return HandleError(logger, exp, "Create board error", code: StatusCodes.Status500InternalServerError);
            }
        }
    }
}

using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Abstractions.Services.Boards;
using DELAY.Core.Application.Abstractions.Services.Common;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Application.Contracts.Models.Dtos;
using DELAY.Core.Application.Contracts.Models.Dtos.Base;
using DELAY.Presentation.RestAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace DELAY.Core.Application.Abstractions.Services
{
    [ApiController]
    [Route("api/tickets-lists")]
    public class TicketsListController : AuthorizedController
    {
        /// <summary>
        /// <inheritdoc cref="IBoardService"/>
        /// </summary>
        private readonly IBoardService boardService;

        /// <summary>
        /// <inheritdoc cref="ILogger"/>
        /// </summary>
        private readonly ILogger<TicketsListController> logger;

        /// <summary>
        /// <inheritdoc cref="IModelMapperService"/>
        /// </summary>
        private readonly IModelMapperService mapper;

        public TicketsListController(IBoardService userService, IModelMapperService mapper, ITokensService tokensService, ILogger<TicketsListController> logger) : base(tokensService)
        {
            this.logger = logger;

            this.boardService = userService ?? throw new ArgumentNullException(nameof(IBoardService));

            this.mapper = mapper ?? throw new ArgumentNullException(nameof(IModelMapperService));
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

                var model = await boardService.GetTicketsListAsync(new TicketsListRequestDto() { TicketsListId = id, BoardId = boardId }, user);

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

        [HttpGet, Route("by-board")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByBoardAsync([FromQuery] Guid boardId)
        {
            return Ok(new List<TicketsListDto>()
            {
                new TicketsListDto(new List<KeyNameDto>()
                {
                    new KeyNameDto(Guid.NewGuid(), "Make one"),
                    new KeyNameDto(Guid.NewGuid(), "Make two"),
                    new KeyNameDto(Guid.NewGuid(), "Make three"),
                }, boardId){Name = "Ticket list simple"},
                new TicketsListDto(new List<KeyNameDto>()
                {
                    new KeyNameDto(Guid.NewGuid(), "Make one"),
                    new KeyNameDto(Guid.NewGuid(), "Make two"),
                }, boardId){Name = "Ticket list super"},
                new TicketsListDto(new List<KeyNameDto>()
                {
                    new KeyNameDto(Guid.NewGuid(), "Make one"),
                    new KeyNameDto(Guid.NewGuid(), "Make two"),
                    new KeyNameDto(Guid.NewGuid(), "Make three"),
                    new KeyNameDto(Guid.NewGuid(), "Make one"),
                    new KeyNameDto(Guid.NewGuid(), "Make two"),
                    new KeyNameDto(Guid.NewGuid(), "Make three"),
                    new KeyNameDto(Guid.NewGuid(), "Make one"),
                    new KeyNameDto(Guid.NewGuid(), "Make two"),
                    new KeyNameDto(Guid.NewGuid(), "Make three"),
                }, boardId){Name = "Ticket list ultra"},
            });
            try
            {
                TryGetUser(out OperationUserInfo user);

                var model = await boardService.GetTicketsListByBoardAsync(boardId, user);

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

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync([FromBody] TicketsListDto model)
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var result = await boardService.CreateTicketsListAsync(model, user);

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
        public async Task<IActionResult> UpdateAsync([FromBody] TicketsListDto model)
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var result = await boardService.UpdateTicketsListAsync(model, user);

                return Ok();
            }
            catch (Exception exp)
            {
                return HandleError(logger, exp, "Update list error", code: StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromBody] Guid id)
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var result = await boardService.DeleteTicketsListAsync(id, user);

                return Ok();
            }
            catch (Exception exp)
            {
                return HandleError(logger, exp, "Delete list error", code: StatusCodes.Status500InternalServerError);
            }
        }
    }
}

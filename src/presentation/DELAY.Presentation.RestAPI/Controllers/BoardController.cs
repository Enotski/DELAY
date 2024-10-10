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
        private readonly ILogger<UserController> logger;

        /// <summary>
        /// <inheritdoc cref="IModelMapperService"/>
        /// </summary>
        private readonly IModelMapperService mapper;

        public BoardController(IBoardService userService, IModelMapperService mapper, ITokensService tokensService, ILogger<UserController> logger) : base(tokensService)
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
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var model = await boardService.GetAsync(id, user);

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

        [HttpGet, Route("/by-user")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByUserAsync()
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var model = await boardService.GetByUserAsync(user.Id);

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

        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="model"><inheritdoc cref="UserDto"/></param>
        /// <returns>User's key</returns>
        /// <remarks>
        /// Request:
        ///
        ///     POST /api/users
        ///     {
        ///       "name": "string",
        ///       "email": "string",
        ///       "phoneNumber": "string",
        ///       "description": "string",
        ///       "password": "string"
        ///     }
        ///
        /// Response:
        /// 
        ///     "00000000-0000-0000-0000-000000000000"
        /// 
        /// </remarks>
        /// <response code="201">User added</response>
        /// <response code="500">Server error</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBoard([FromBody] NameDto model)
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var result = await boardService.CreateAsync(model, user);

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
        public async Task<IActionResult> UpdateBoard([FromBody] EditBoardRequestDto model)
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var result = await boardService.UpdateAsync(model, user);

                return Ok();
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
        public async Task<IActionResult> DeleteBoard([FromBody] Guid id)
        {
            try
            {
                TryGetUser(out OperationUserInfo user);

                var result = await boardService.DeleteAsync(id, user);

                return Ok();
            }
            catch (Exception exp)
            {
                return HandleError(logger, exp, "Create board error", code: StatusCodes.Status500InternalServerError);
            }
        }
    }
}

using DELAY.Core.Application.Abstractions.Mapper;
using DELAY.Core.Application.Abstractions.Services.Tickets;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Extensions;
using DELAY.Presentation.RestAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace DELAY.Core.Application.Abstractions.Services
{
    [ApiController]
    [Route("boards")]
    public class BoardController : BaseController
    {
        /// <summary>
        /// <inheritdoc cref="IUserService"/>
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

        public BoardController(IBoardService userService, IModelMapperService mapper, ILogger<UserController> logger)
        {
            this.logger = logger;

            this.boardService = userService ?? throw new ArgumentNullException(nameof(IBoardService));

            this.mapper = mapper ?? throw new ArgumentNullException(nameof(IModelMapperService));
        }

        /// <summary>
        /// Получение активности по ключу
        /// </summary>
        /// <param name="id">Ключ записи</param>
        /// <returns><inheritdoc cref="UserDto"/></returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /api/users/{id}
        ///     
        /// Пример ответа:
        /// 
        ///     {
        ///       "id": "00000000-0000-0000-0000-000000000000",
        ///       "beginTime": "0001-01-01T00:00:00",
        ///       "endTime": "0001-01-01T00:00:00",
        ///       "type": 0,
        ///       "description": "string",
        ///       "triggeredBy": "string"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="404">Не удалось найти по ключу</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                var user = await boardService.GetAsync(id);

                if (user == null)
                {
                    string message = "Не удалось определить объект активности по ключу";

                    logger.LogInformation(message + " {id}", id);

                    return Problem(message, statusCode: StatusCodes.Status404NotFound);
                }

                var result = mapper.Map<UserDto>(user);

                logger.LogInformation("Получение объекта активности по ключу {id}: {result}", result, id);

                return Ok(result);
            }
            catch (Exception exp)
            {
                logger.LogError("Ошибка получения объекта активности по ключу {id}: {exp}", id, exp);

                return Problem(exp.Format(), statusCode: StatusCodes.Status500InternalServerError);
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
        public async Task<IActionResult> AddBoard([FromBody] BoardDto model)
        {
            try
            {
                var result = await boardService.AddAsync(mapper.Map<Board>(model), HttpContext.User.Identity.Name);

                logger.LogInformation("Создание объекта активности по параметрам и получение ключа {result} новой записи", result);

                return Created(result.ToString(), result);
            }
            catch (Exception exp)
            {
                logger.LogError("Ошибка создания объекта активности по параметрам: {exp}", exp);

                return Problem(exp.Format(), statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}

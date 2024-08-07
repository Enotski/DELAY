using DELAY.Core.Application.Abstractions.Mapper;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Extensions;
using DELAY.Presentation.RestAPI.Contracts.Request;
using DELAY.Presentation.RestAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace DELAY.Core.Application.Abstractions.Services
{
    [ApiController]
    [Route("api/users")]
    public class UserController : BaseController
    {
        /// <summary>
        /// <inheritdoc cref="IUserService"/>
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// <inheritdoc cref="ILogger"/>
        /// </summary>
        private readonly ILogger<UserController> logger;

        /// <summary>
        /// <inheritdoc cref="IModelMapperService"/>
        /// </summary>
        private readonly IModelMapperService mapper;

        public UserController()
        {
        }

        /// <summary>
        /// Получение активности по ключу
        /// </summary>
        /// <param name="id">Ключ записи</param>
        /// <returns><inheritdoc cref="UserApiModel"/></returns>
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
                var user = await userService.GetAsync(id);

                if (user == null)
                {
                    string message = "Не удалось определить объект активности по ключу";

                    logger.LogInformation(message + " {id}", id);

                    return Problem(message, statusCode: StatusCodes.Status404NotFound);
                }

                var result = mapper.Map<UserApiModel>(user);

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
        /// Получение активности по ключу
        /// </summary>
        /// <param name="id">Ключ записи</param>
        /// <returns><inheritdoc cref="UserApiModel"/></returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /api/users/board/{id}
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
        [Route("board")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByBoardAsync(Guid id)
        {
            try
            {
                var user = await userService.GetBoardUsersAsync(id);

                if (user == null)
                {
                    string message = "Не удалось определить объект активности по ключу";

                    logger.LogInformation(message + " {id}", id);

                    return Problem(message, statusCode: StatusCodes.Status404NotFound);
                }

                var result = mapper.Map<UserApiModel>(user);

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
        /// Получение активности по ключу
        /// </summary>
        /// <param name="id">Ключ записи</param>
        /// <returns><inheritdoc cref="UserApiModel"/></returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     GET /api/users/ticket/{id}
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
        [Route("ticket")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByTicketAsync(Guid id)
        {
            try
            {
                var user = await userService.GetUsersByTicketAsync(id);

                if (user == null)
                {
                    string message = "Не удалось определить объект активности по ключу";

                    logger.LogInformation(message + " {id}", id);

                    return Problem(message, statusCode: StatusCodes.Status404NotFound);
                }

                var result = mapper.Map<UserApiModel>(user);

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
        /// Получение страницы активностей по заданным параметрам
        /// </summary>
        /// <param name="options"><inheritdoc cref="PagedDataRequest"/></param>
        /// <returns><inheritdoc cref="PagedDataApiModel"/></returns>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /api/events/get-all
        ///     {
        ///        "offset": 0,
        ///        "limit": 0,
        ///        "filter": "string",
        ///        "sort": [
        ///           "string"
        ///        ]
        ///     }
        ///
        /// Пример ответа:
        /// 
        ///     {
        ///       "events": [
        ///         {
        ///           "id": "00000000-0000-0000-0000-000000000000",
        ///           "beginTime": "0001-01-01T00:00:00",
        ///           "endTime": "0001-01-01T00:00:00",
        ///           "type": 0,
        ///           "description": null,
        ///           "triggeredBy": null
        ///         }
        ///       ],
        ///       "hasNext": true,
        ///       "hasPrev": false
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">OK</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpPost]
        [Route("get-all")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync([FromBody] PagedDataRequest options)
        {
            try
            {
                var data = await userService.GetRecordsAsync(options.Searchs, options.Sorts, options.Pagination);

                var result = mapper.Map<PagedDataApiModel<UserApiModel>>(data);

                logger.LogInformation("Получение страницы активностей по заданным параметрам {options}: {result}", result, options);

                return Ok(result);
            }
            catch (Exception exp)
            {
                logger.LogError("Ошибка получения страницы активностей по заданным параметрам {options}: {exp}", options, exp);

                return Problem(exp.Format(), statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Получение активности по ключу
        /// </summary>
        /// <param name="id">Ключ записи</param>
        /// <returns><inheritdoc cref="UserApiModel"/></returns>
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
        [Route("get-list")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromBody] IEnumerable<Guid> ids)
        {
            try
            {
                var user = await userService.GetRecordsAsync(ids);

                if (user == null)
                {
                    string message = "Не удалось определить объект активности по ключу";

                    logger.LogInformation(message + " {ids}", ids);

                    return Problem(message, statusCode: StatusCodes.Status404NotFound);
                }

                var result = mapper.Map<UserApiModel>(user);

                logger.LogInformation("Получение объекта активности по ключу {ids}: {result}", result, ids);

                return Ok(result);
            }
            catch (Exception exp)
            {
                logger.LogError("Ошибка получения объекта активности по ключу {ids}: {exp}", ids, exp);

                return Problem(exp.Format(), statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Получение активности по ключу
        /// </summary>
        /// <param name="id">Ключ записи</param>
        /// <returns><inheritdoc cref="UserApiModel"/></returns>
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
        [Route("get-key-name-list")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetKeyNamesAsync([FromBody] IEnumerable<Guid> ids)
        {
            try
            {
                var user = await userService.GetKeyNameRecordsAsync(ids);

                if (user == null)
                {
                    string message = "Не удалось определить объект активности по ключу";

                    logger.LogInformation(message + " {ids}", ids);

                    return Problem(message, statusCode: StatusCodes.Status404NotFound);
                }

                var result = mapper.Map<UserApiModel>(user);

                logger.LogInformation("Получение объекта активности по ключу {ids}: {result}", result, ids);

                return Ok(result);
            }
            catch (Exception exp)
            {
                logger.LogError("Ошибка получения объекта активности по ключу {ids}: {exp}", ids, exp);

                return Problem(exp.Format(), statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="model"><inheritdoc cref="UserApiModel"/></param>
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
        public async Task<IActionResult> AddUser([FromBody] UserApiModel model)
        {
            try
            {
                var result = await userService.AddAsync(mapper.Map<User>(model), HttpContext.User.Identity.Name);

                logger.LogInformation("Создание объекта активности по параметрам и получение ключа {result} новой записи", result);

                return Created(result.ToString(), result);
            }
            catch (Exception exp)
            {
                logger.LogError("Ошибка создания объекта активности по параметрам: {exp}", exp);

                return Problem(exp.Format(), statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="model"><inheritdoc cref="UserApiModel"/></param>
        /// <returns>Updated count</returns>
        /// <remarks>
        /// Request:
        ///
        ///     PUT /api/users
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
        ///     "0"
        /// 
        /// </remarks>
        /// <response code="202">User updated</response>
        /// <response code="500">Server error</response>
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser([FromBody] UserApiModel model)
        {
            try
            {
                var result = await userService.UpdateAsync(mapper.Map<User>(model), HttpContext.User.Identity.Name);

                logger.LogInformation("Создание объекта активности по параметрам и получение ключа {result} новой записи", result);

                return Created(result.ToString(), result);
            }
            catch (Exception exp)
            {
                logger.LogError("Ошибка создания объекта активности по параметрам: {exp}", exp);

                return Problem(exp.Format(), statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Удаление активностей по параметрам фильтра
        /// </summary>
        /// <param name="id">Фильтр записей для удаления</param>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     DELETE /api/events/{id}
        ///
        /// Пример ответа:
        /// 
        ///     0
        /// 
        /// </remarks>
        /// <response code="202">Операция выполнена</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var deletedCount = await userService.DeleteAsync(id, HttpContext.User.Identity.Name);

                logger.LogInformation("Удалено {deletedCount} активностей по параметрам фильтра {filter}", deletedCount, id);

                return Accepted(deletedCount);
            }
            catch (Exception exp)
            {
                logger.LogError("Ошибка удаления активностей по параметрам фильтра {filter}: {exp}", filter, exp);

                return Problem(exp.Format(), statusCode: StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Удаление активностей по параметрам фильтра
        /// </summary>
        /// <param name="id">Фильтр записей для удаления</param>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /api/events/delete-multiple
        ///     {
        ///       "ids": ["string"],
        ///     }
        ///     
        /// Пример ответа:
        /// 
        ///     0
        /// 
        /// </remarks>
        /// <response code="202">Операция выполнена</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpPost]
        [Route("delete-multiple")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync([FromBody] IEnumerable<Guid> ids)
        {
            try
            {
                var deletedCount = await userService.DeleteAsync(ids, HttpContext.User.Identity.Name);

                logger.LogInformation("Удалено {deletedCount} активностей по параметрам фильтра {filter}", deletedCount, id);

                return Accepted(deletedCount);
            }
            catch (Exception exp)
            {
                logger.LogError("Ошибка удаления активностей по параметрам фильтра {filter}: {exp}", filter, exp);

                return Problem(exp.Format(), statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}

using DELAY.Core.Application.Abstractions.Mapper;
using DELAY.Core.Application.Contracts.Models;
using DELAY.Core.Domain.Models;
using DELAY.Infrastructure.Cryptography;
using DELAY.Infrastructure.Extensions;
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

        private readonly ICryptographyService cryptoService;

        public UserController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                return Ok();
            }
            catch (Exception exp)
            {
                return Ok();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                return Ok();
            }
            catch (Exception exp)
            {
                return Ok();
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
        public async Task<IActionResult> AddUser([FromBody] UserDto model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException(nameof(UserDto));

                var triggered = await userService.ValidatePermissionToOperation(Domain.Enums.RoleType.Administrator, HttpContext.User.Identity.Name);

                var passwordHash = cryptoService.GetHash(model.Password);

                var newUser = new User(model.Name, model.Email, model.PhoneNumber, passwordHash, triggered.Name);

                var result = await userService.AddAsync(newUser, triggered.Name);

                logger.LogInformation("Создание объекта активности по параметрам {model} и получение ключа {result} новой записи", newUser, result);

                return Created(result.ToString(), result);
            }
            catch (Exception exp)
            {
                logger.LogError("Ошибка создания объекта активности по параметрам {model}: {exp}", model, exp);

                return Problem(exp.Format(), statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}

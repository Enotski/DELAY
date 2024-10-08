using DELAY.Core.Application.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DELAY.Presentation.RestAPI.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        public string UserIdentityName
        {
            get {
                return "admin";
                if (HttpContext is null)
                {
                    return null;
                }
                var name = HttpContext.User.Identity.Name;
                if (string.IsNullOrWhiteSpace(name))
                {
                    name = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
                }
                return name?.ToUpper();
            }
        }
        public string RemoteIpAddress
        {
            get {
                if (HttpContext is null)
                {
                    return null;
                }
                return HttpContext.Connection.RemoteIpAddress?.ToString();
            }
        }
        public string UserAgentData
        {
            get {
                if (HttpContext is null)
                {
                    return null;
                }
                return HttpContext.Request.Headers["User-Agent"].ToString();
            }
        }

        /// <summary>
        /// Обработка исключения и запись в лог
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="ex">Объект исключения, будет записан в лог</param>
        /// <param name="logMessage">Сообщение</param>
        /// <param name="logParams">Параметры лога помимо объекта исключения</param>
        /// <returns></returns>
        protected IActionResult HandleError(ILogger logger, Exception ex, string logMessage, int code = StatusCodes.Status500InternalServerError, params object[] logParams)
        {
            logger.LogError(logMessage + ": {ex}", ex, logParams);

#if DEBUG
            return Problem(ex.Format(), statusCode: code);
#else
            return Problem(ex.Message, statusCode: code);
#endif
        }
    }
}

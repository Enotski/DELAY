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
    }
}

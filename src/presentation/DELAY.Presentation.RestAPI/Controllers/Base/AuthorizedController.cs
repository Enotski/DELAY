using Microsoft.AspNetCore.Authorization;

namespace DELAY.Presentation.RestAPI.Controllers.Base
{
    [Authorize(AuthenticationSchemes = "Jwt")]
    public class AuthorizedController : BaseController
    {
    }
}

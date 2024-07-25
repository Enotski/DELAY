using DELAY.Presentation.RestAPI.Controllers;
using DELAY.Presentation.RestAPI;
using Microsoft.AspNetCore.Mvc;

namespace DELAY.Core.Application.Abstractions.Services
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        public UserController()
        {
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
    }
}

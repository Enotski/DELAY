using DELAY.Presentation.RestAPI.Contracts.Request;
using DELAY.Presentation.RestAPI.Controllers.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DELAY.Presentation.RestAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly IAuthenticationService authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            ArgumentNullException.ThrowIfNull(authenticationService, nameof(IAuthenticationService));

            this.authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("login-form")]
        public async Task<IActionResult> LoginFormAsync([FromBody] AuthRequestDto options)
        {
            return Ok();
            //try
            //{
            //    var model = await _serviceManager.AuthenticationService.LoginFormAsync(options, RemoteIpAddress, UserAgentData);
            //    if (model is null || model.Result == ResponseResultType.Error || model.Data is null)
            //    {
            //        return Unauthorized();
            //    }
            //    return Ok(model);
            //}
            //catch (Exception exp)
            //{
            //    return Unauthorized(exp.Message);
            //}
        }

        [HttpPost]
        [Route("register-form")]
        public async Task<IActionResult> RegisterFormAsync([FromBody] AuthRequestDto options)
        {
            return Ok();
            //try
            //{
            //    var model = await _serviceManager.AuthenticationService.RegisterFormAsync(options, RemoteIpAddress, UserAgentData);
            //    if (model is null || model.Result == ResponseResultType.Error || model.Data is null)
            //    {
            //        return Unauthorized();
            //    }
            //    return Ok(model);
            //}
            //catch (Exception exp)
            //{
            //    return Unauthorized(exp.Message);
            //}
        }

        [HttpPost]
        [Route("login-google")]
        public IActionResult LoginGoogle()
        {
            var properties = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
            //try
            //{
            //    var model = await _serviceManager.AuthenticationService.LoginFormAsync(options, RemoteIpAddress, UserAgentData);
            //    if (model is null || model.Result == ResponseResultType.Error || model.Data is null)
            //    {
            //        return Unauthorized();
            //    }
            //    return Ok(model);
            //}
            //catch (Exception exp)
            //{
            //    return Unauthorized(exp.Message);
            //}
        }

        [HttpPost]
        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(x =>
            new
            {
                x.Issuer,
                x.OriginalIssuer,
                x.Type,
                x.Value
            });

            return Ok(result);
            //try
            //{
            //    var model = await _serviceManager.AuthenticationService.LoginFormAsync(options, RemoteIpAddress, UserAgentData);
            //    if (model is null || model.Result == ResponseResultType.Error || model.Data is null)
            //    {
            //        return Unauthorized();
            //    }
            //    return Ok(model);
            //}
            //catch (Exception exp)
            //{
            //    return Unauthorized(exp.Message);
            //}
        }
    }
}

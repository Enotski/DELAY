using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Contracts.Models.Auth;
using DELAY.Presentation.RestAPI.Contracts.Response;
using DELAY.Presentation.RestAPI.Controllers.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DELAY.Presentation.RestAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        private readonly TokensSettings tokensSettings;

        public AuthController(IAuthService authService, IOptions<TokensSettings> tokensSettings)
        {
            ArgumentNullException.ThrowIfNull(authService, nameof(IAuthenticationService));

            _authService = authService;
            this.tokensSettings = tokensSettings.Value;
        }

        private AuthResponseDto CreateAuthResponse(AuthResult authResult)
        {
            var endpoints = new List<string>();

            if (authResult.Role == Core.Domain.Enums.RoleType.None)
            {
                endpoints.Add("home");
            }
            else if (authResult.Role == Core.Domain.Enums.RoleType.User)
            {
                endpoints.AddRange(["home", "boards", "rooms", "account"]);
            }
            else if (authResult.Role == Core.Domain.Enums.RoleType.Admin)
            {
                endpoints.AddRange(["home", "boards", "rooms", "account", "users"]);
            }

            return new AuthResponseDto(new TokensResponseDto(authResult.Tokens.AccessToken), endpoints);
        }

        private void SetTokensInsideCookie(Tokens tokenDto, HttpContext context)
        {
            context.Response.Cookies.Append("refresh_token", tokenDto.RefreshToken,
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(tokensSettings.RefreshTokenExpirationDays),
                    HttpOnly = true,
                    IsEssential = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });
        }

        [HttpPost]
        [Route("refresh-tokens")]
        public async Task<IActionResult> RefreshTokensAsync()
        {
            try
            {
                HttpContext.Request.Cookies.TryGetValue("refresh_token", out var refreshToken);

                var tokens = await _authService.RefreshTokensAsync(refreshToken);

                if (tokens is null)
                {
                    return Unauthorized();
                }

                SetTokensInsideCookie(tokens, HttpContext);

                return Ok(new TokensResponseDto(tokens.AccessToken));
            }
            catch (Exception exp)
            {
                return Unauthorized(exp.Message);
            }
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignInAsync([FromBody] SignInRequest model)
        {
            try
            {
                model.SetUserAgentData(UserAgentData, RemoteIpAddress);

                var authResultModel = await _authService.SignInAsync(model);

                if (authResultModel is null)
                {
                    return Unauthorized();
                }

                SetTokensInsideCookie(authResultModel.Tokens, HttpContext);

                return Ok(CreateAuthResponse(authResultModel));
            }
            catch (Exception exp)
            {
                return Unauthorized(exp.Message);
            }
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUpAsync([FromBody] SignUpRequest model)
        {
            try
            {
                model.SetUserAgentData(UserAgentData, RemoteIpAddress);

                var authResultModel = await _authService.SignUpAsync(model);

                if (authResultModel is null)
                {
                    return Unauthorized();
                }

                SetTokensInsideCookie(authResultModel.Tokens, HttpContext);

                return Ok(CreateAuthResponse(authResultModel));
            }
            catch (Exception exp)
            {
                return Unauthorized(exp.Message);
            }
        }

        [HttpPost]
        [Route("signin-google")]
        public async Task<IActionResult> SignInGoogle([FromBody] GoogleAuthRequest model)
        {
            try
            {
                model.SetUserAgentData(UserAgentData, RemoteIpAddress);

                var authResultModel = await _authService.SignInGoogleAsync(model);

                if (authResultModel is null)
                {
                    return Unauthorized();
                }

                SetTokensInsideCookie(authResultModel.Tokens, HttpContext);

                return Ok(CreateAuthResponse(authResultModel));
            }
            catch (Exception exp)
            {
                return Unauthorized(exp.Message);
            }
        }
        [HttpPost]
        [Route("signin-vk")]
        public async Task<IActionResult> SignInVk([FromBody] VkAuthRequest model)
        {
            try
            {
                model.SetUserAgentData(UserAgentData, RemoteIpAddress);

                var authResultModel = await _authService.SignInVkAsync(model);

                if (authResultModel is null)
                {
                    return Unauthorized();
                }

                SetTokensInsideCookie(authResultModel.Tokens, HttpContext);

                return Ok(CreateAuthResponse(authResultModel));
            }
            catch (Exception exp)
            {
                return Unauthorized(exp.Message);
            }
        }
        [HttpPost]
        [Route("signout")]
        public IActionResult SignOut()
        {
            try
            {
                HttpContext.Request.Cookies.TryGetValue("refresh_token", out string refreshToken);

                _authService.SignOut(refreshToken);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Route("signout-all")]
        public IActionResult SignOutAll()
        {
            try
            {
                HttpContext.Request.Cookies.TryGetValue("refresh_token", out string refreshToken);

                _authService.SignOutAll(refreshToken);

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}

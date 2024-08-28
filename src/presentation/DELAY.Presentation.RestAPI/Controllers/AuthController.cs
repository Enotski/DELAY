using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Contracts.Models.Auth;
using DELAY.Presentation.RestAPI.Contracts.Response;
using DELAY.Presentation.RestAPI.Controllers.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

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
            var endpoints = new List<ApiEndpoint>();

            if (authResult.Role == Core.Domain.Enums.RoleType.User)
            {
                endpoints.AddRange([
                    new ApiEndpoint("tickets", "Tickets"),
                    new ApiEndpoint("rooms", "Rooms"),
                    new ApiEndpoint("account", "Account"),
                    new ApiEndpoint("users", "Users")
                ]);
            }
            else if (authResult.Role == Core.Domain.Enums.RoleType.Admin)
            {
                endpoints.AddRange([
                    new ApiEndpoint("tickets", "Tickets"),
                    new ApiEndpoint("rooms", "Rooms"),
                    new ApiEndpoint("account", "Account"),
                    new ApiEndpoint("users", "Users"),
                ]);
            }

            return new AuthResponseDto(new TokensResponseDto(authResult.Tokens.AccessToken), endpoints);
        }

        [HttpGet]
        [Route("signin-transient")]
        public async Task<IActionResult> TransientSingInByRefreshTokenAsync(string fingerPrint)
        {
            try
            {
                HttpContext.Request.Cookies.TryGetValue("refresh_token", out var refreshToken);

                var authResultModel = await _authService.TransientSingInByRefreshTokenAsync(refreshToken, new AuthUserAgentRequest(fingerPrint, RemoteIpAddress, UserAgentData));

                if (authResultModel is null)
                {
                    return Unauthorized();
                }

                SetRefreshTokensInsideCookie(authResultModel.Tokens.RefreshToken, HttpContext);

                return Ok(CreateAuthResponse(authResultModel));
            }
            catch (Exception exp)
            {
                return Unauthorized(exp.Message);
            }
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

                SetRefreshTokensInsideCookie(tokens.RefreshToken, HttpContext);

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

                SetRefreshTokensInsideCookie(authResultModel.Tokens.RefreshToken, HttpContext);

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

                SetRefreshTokensInsideCookie(authResultModel.Tokens.RefreshToken, HttpContext);

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

                SetRefreshTokensInsideCookie(authResultModel.Tokens.RefreshToken, HttpContext);

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

                SetRefreshTokensInsideCookie(authResultModel.Tokens.RefreshToken, HttpContext);

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
                if(HttpContext.Request.Cookies["refresh_token"] != null)
                {
                    var token = HttpContext.Request.Cookies["refresh_token"];

                    _authService.SignOut(token);

                    SetRefreshTokensInsideCookie("", HttpContext, -1);
                }
                else
                {
                    return BadRequest("Tokens not passed");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        private void SetRefreshTokensInsideCookie(string token, HttpContext context, int? expirationDays = null)
        {
            expirationDays = expirationDays ?? (int)tokensSettings.RefreshTokenExpirationDays;

            context.Response.Cookies.Append("refresh_token", token,
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(expirationDays.Value),
                    HttpOnly = true,
                    IsEssential = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Domain = "localhost",
                    Path = "/api/auth"
                });
        }

        [HttpPost]
        [Route("signout-all")]
        public IActionResult SignOutAll()
        {
            try
            {
                if (HttpContext.Request.Cookies["refresh_token"] != null)
                {
                    var token = HttpContext.Request.Cookies["refresh_token"];

                    _authService.SignOutAll(token);

                    SetRefreshTokensInsideCookie("", HttpContext, -1);
                }
                else
                {
                    return BadRequest("Tokens not passed");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}

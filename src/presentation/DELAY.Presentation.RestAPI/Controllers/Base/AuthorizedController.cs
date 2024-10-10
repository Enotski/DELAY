using DELAY.Core.Application.Abstractions.Services.Auth;
using DELAY.Core.Application.Contracts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace DELAY.Presentation.RestAPI.Controllers.Base
{
    [Authorize(AuthenticationSchemes = "Jwt")]
    public class AuthorizedController : BaseController
    {
        private readonly ITokensService _tokensService;

        public AuthorizedController(ITokensService tokensService)
        {
            ArgumentNullException.ThrowIfNull(tokensService, nameof(ITokensService));
            _tokensService = tokensService;
        }

        protected bool TryGetUser(out OperationUserInfo userInfo)
        {
            userInfo = null;

            if (HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues tokenValue))
            {
                var token = tokenValue.FirstOrDefault();

                var claims = _tokensService.GetPrincipal(token, out DateTime validTo)?.Claims;
                if(claims == null)
                    return false;

                var id = Guid.Parse(claims.FirstOrDefault(x => x.Type == "ueid").Value);
                var name = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;

                userInfo = new OperationUserInfo(id, name, email);

                return true;
            }

            return false;
        }
    }
}

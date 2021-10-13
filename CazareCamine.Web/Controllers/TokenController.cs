using CazareCamine.Data.Context;
using CazareCamine.Data.Services.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CazareCamine.Web.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly UserContext context;
        private readonly ITokenService tokenService;

        public TokenController(UserContext _context, ITokenService _tokenService)
        {
            context = _context;
            tokenService = _tokenService;
        }

        [HttpGet]
        public IActionResult Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;

            var principal = tokenService.GetPrincipalFromExpiredToken(accessToken);
            string email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

            return Ok();

        }

    }

    public class TokenApiModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

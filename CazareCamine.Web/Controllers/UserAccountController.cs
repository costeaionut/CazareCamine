using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CazareCamine.Data.Entities.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using CazareCamine.Data.Entities.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CazareCamine.Data.Services;
using CazareCamine.Data.Services.Token;

namespace CazareCamine.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserAccountController : Controller
    {
        private readonly IMapper mapper;
        private readonly UserManager<UserModel> userManager;
        private readonly IConfiguration configuration;
        private readonly IRoleManager roleManager;
        private readonly ITokenService tokenService;

        public UserAccountController(
            IMapper Mapper, 
            UserManager<UserModel> UserManager, 
            IConfiguration Configuration,
            IRoleManager RoleManager,
            ITokenService TokenService)
        {
            userManager = UserManager;
            mapper = Mapper;
            configuration = Configuration;
            roleManager = RoleManager;
            tokenService = TokenService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDTO userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            var user = mapper.Map<UserModel>(userForRegistration);
            var result = await userManager.CreateAsync(user, userForRegistration.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new RegistrationResponseDTO { Errors = errors });
            }

            return StatusCode(201);
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] UserForLoginDTO userForLogin)
        {

            if(userForLogin == null)
                return BadRequest("Invalid client request");
            
            UserModel user = await userManager.FindByEmailAsync(userForLogin.Email);
            if(user == null)
                return Unauthorized("Email is invalid!");
            

            var passwordCheck = await userManager.CheckPasswordAsync(user, userForLogin.Password);
            if(!passwordCheck)
                return Unauthorized("Password is invalid!");

            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.LastName + ", " + user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            List<string> roles = roleManager.GetUserRoles(user);
            foreach(string role in roles)
            {
                Claim roleClaim = new Claim(ClaimTypes.Role, role);
                userClaims.Add(roleClaim);
            }

            var accessToken = tokenService.GenerateAccessToken(userClaims);
            var refreshToken = tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpirationTime = DateTime.Now.AddDays(7);

            return Ok(new { accessToken, refreshToken });
        }

        [HttpGet]
        public IActionResult GetCurrentUser()
        {
            CurrentUser user = new();

            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            if (claims.Count() == 0)
            {
                user.IsAuthenticated = false;
                return Ok(user);
            }

            //Add Roles
            var userRoleClaims = claims.Where(c => c.Type == ClaimTypes.Role);
            foreach(var claim in userRoleClaims)
                user.Roles.Add(claim.Value);

            //Add Email
            user.Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

            //Add First + Last Name
            string fullname = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
            user.LastName = fullname.Split(',')[0];
            user.FirstName = fullname.Split(',')[1];

            return Ok(user);
        }
    }
}

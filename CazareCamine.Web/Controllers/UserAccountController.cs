using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CazareCamine.Data.Entities.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using CazareCamine.Data.Entities.DTO;

namespace CazareCamine.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserAccountController : Controller
    {
        private readonly IMapper mapper;
        private readonly UserManager<UserModel> userManager;

        public UserAccountController(IMapper Mapper, UserManager<UserModel> UserManager)
        {
            userManager = UserManager;
            mapper = Mapper;
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

    }
}

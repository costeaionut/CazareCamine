using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CazareCamine.Data.Services;
using CazareCamine.Data.Models;

namespace CazareCamine.Web.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserAccountController : Controller
    {

        IUserService userService;

        public UserAccountController(IUserService UserService)
        {
            userService = UserService;
        }

        [HttpGet]
        public IActionResult GetAllUsers() =>
            Ok(userService.GetAllUsers());

        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            UserModel user = userService.GetUserById(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] UserModel user)
        {
            userService.RegisterUser(user);
            return Ok();
        }

    }
}

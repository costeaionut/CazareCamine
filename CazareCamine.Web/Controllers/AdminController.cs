using CazareCamine.Data.Context;
using CazareCamine.Data.Entities.Models;
using CazareCamine.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CazareCamine.Data.Entities.DTO;

namespace CazareCamine.Web.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserContext context;
        private readonly UserManager<UserModel> userManager;
        private readonly IRoleManager roleManager;

        public AdminController(UserContext _context, UserManager<UserModel> _userManager, IRoleManager _roleManager)
        {
            context = _context;
            userManager = _userManager;
            roleManager = _roleManager;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = context.Users.ToList();
            List<UserInfo> userInfo = new();
            foreach(UserModel user in users)
            {
                UserInfo ui = new();

                ui.Id = user.Id;
                ui.Email = user.Email;
                ui.Fullname = user.LastName + ", " + user.FirstName;
                ui.Roles = roleManager.GetUserRoles(user);

                userInfo.Add(ui);
            }

            return Ok(userInfo);
        }

    }
}

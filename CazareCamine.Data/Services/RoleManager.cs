using CazareCamine.Data.Context;
using CazareCamine.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CazareCamine.Data.Services
{
    public class RoleManager : IRoleManager
    {
        private readonly UserContext context;
        public RoleManager(UserContext _context)
        {
            context = _context;
        }

        public List<string> GetUserRoles(UserModel user)
        {
            List<string> rolesList = new();

            var rolesEntity = context.UserRole.Where(ur => ur.UserId == user.Id).ToList();

            foreach(var role in rolesEntity)
                rolesList.Add(context.Role.FirstOrDefault(r => r.Id == role.RoleId).Role);

            return rolesList;
        }
    }
}

using CazareCamine.Data.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CazareCamine.Data.Services
{
    public interface IRoleManager
    {
        public List<string> GetUserRoles(UserModel user);
    }
}

using CazareCamine.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CazareCamine.Data.Services
{
    public interface IUserService
    {
        public void RegisterUser(UserModel user);
        public UserModel GetUserById(int id);
        public List<UserModel> GetAllUsers();
        public void ModifyUser(UserModel user);
        public void DeleteUser(UserModel user);
    }
}

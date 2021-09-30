using CazareCamine.Data.Context;
using CazareCamine.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CazareCamine.Data.Services
{
    public class UserService : IUserService
    {
        UserContext context;

        public UserService(UserContext Context)
        {
            context = Context;
        }

        public List<UserModel> GetAllUsers() =>
            context.Users.ToList();


        public UserModel GetUserById(int id) => 
            context.Users.FirstOrDefault(u => u.UserId == id);


        public void RegisterUser(UserModel user) 
        {
            context.Users.Add(user);
            context.SaveChanges();
        }


        public void ModifyUser(UserModel user)
        {
            UserModel userToBeModified = this.GetUserById(user.UserId);
            this.DeleteUser(userToBeModified);
            this.RegisterUser(user);
        }

        public void DeleteUser(UserModel user)
        {
            context.Remove(user);
            context.SaveChanges();
        }

    }
}

using pawbossAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pawbossAPI.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        bool CreateUser(User user);
        bool UpdateUser(UserUpdate user);
        bool DeleteUser(int id);
        User GetUser(int id);
        User GetUserByUserName(string userName);
        void UpdateStatusLoggedIn(string userName);
        void UpdateStatusLoggedOut(string userName);
    }
}

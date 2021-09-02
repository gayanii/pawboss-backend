using pawbossAPI.DBContexts;
using pawbossAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pawbossAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private PawBossContext db = new PawBossContext();

        public bool CreateUser(User user)
        {
            if (db.User.Any(x => x.Username.Equals(user.Username)))
            {
                return false;
            }
            db.Add(user);
            db.SaveChanges();
            return true;
        }

        public bool DeleteUser(int id)
        {
            var deleteUser = db.User.Where(x => x.Id.Equals(id)).FirstOrDefault();
            db.Remove(deleteUser);
            db.SaveChanges();
            return true;
        }

        public IEnumerable<User> GetUsers()
        {
            var allUsers = db.User.ToList();
            return allUsers;
        }

        public User GetUser(int id)
        {
            try
            {
                var User = db.User.First(x => x.Id.Equals(id));
                if (User != null) return User;
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public User GetUserByUserName(string userName)
        {
            var user = db.User.Where(x => x.Username == userName).FirstOrDefault();
            return user;
        }

        public bool UpdateUser(UserUpdate user)
        {
            var updatingUser = db.User.First(x => x.Id.Equals(user.Id));
            updatingUser.FirstName = user.FirstName;
            updatingUser.LastName = user.LastName;
            updatingUser.Username = user.Username;
            updatingUser.Address = user.Address;
            updatingUser.Email = user.Email;
            updatingUser.ContactNo = user.ContactNo;

            if (db.User.Any(x => x.Username.Equals(user.Username) && !x.Id.Equals(user.Id)))
            {
                return false;
            }

            db.SaveChanges();
            return true;
        }

        public void UpdateStatusLoggedIn(string userName)
        {
            var identifyUser = db.User.Where(x => x.Username.Equals(userName)).FirstOrDefault();
            identifyUser.IsLoggedIn = true;
            db.SaveChanges();
        }

        public void UpdateStatusLoggedOut(string userName)
        {
            var identifyUser = db.User.Where(x => x.Username.Equals(userName)).FirstOrDefault();
            identifyUser.IsLoggedIn = false;
            db.SaveChanges();
        }
    }
}

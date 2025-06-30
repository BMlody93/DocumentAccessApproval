using DocumentAccessApproval.DataLayer;
using DocumentAccessApproval.Domain.Interfaces;
using DocumentAccessApproval.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentAccessApproval.BusinessLogic.Managers
{
    public class UserManager : IUserManager
    {
        public User GetUser(string username)
        {
            using (var dbContext = new DatabaseContext())
            {
                var user = dbContext.Users.FirstOrDefault(ar => ar.Username == username);
                return user;
            }
        }

        public User GetUser(Guid id)
        {
            using (var dbContext = new DatabaseContext())
            {
                var user = dbContext.Users.FirstOrDefault(ar => ar.Id == id);
                return user;
            }
        }

        public IEnumerable<User> GetUsers()
        {
            using (var dbContext = new DatabaseContext())
            {
                var users = dbContext.Users.ToList();
                return users;
            }
        }
    }
}

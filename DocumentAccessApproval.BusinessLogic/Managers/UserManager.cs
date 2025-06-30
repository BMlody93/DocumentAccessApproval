using DocumentAccessApproval.DataLayer;
using DocumentAccessApproval.Domain.Interfaces;
using DocumentAccessApproval.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentAccessApproval.BusinessLogic.Managers
{
    public class UserManager : IUserManager
    {
        private readonly DatabaseContext _dbContext;
        public UserManager(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUserAsync(string username)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(username);
                var user = await _dbContext.Users.SingleOrDefaultAsync(ar => ar.Username == username);

                if (user == null)
                    throw new Exception("User does not exist");

                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(id);

                if (user == null)
                    throw new Exception("User does not exist");

                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            try
            {
                var users = await _dbContext.Users.ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

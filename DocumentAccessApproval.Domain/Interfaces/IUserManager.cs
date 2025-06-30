using DocumentAccessApproval.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentAccessApproval.Domain.Interfaces
{
    public interface IUserManager
    {
        Task<User> GetUserAsync(string username);
        Task<User> GetUserAsync(Guid id);
        Task<IEnumerable<User>> GetUsersAsync();
    }
}

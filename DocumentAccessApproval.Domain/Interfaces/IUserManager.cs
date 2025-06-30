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
        User GetUser(string username);
        User GetUser(Guid id);
        IEnumerable<User> GetUsers();
    }
}

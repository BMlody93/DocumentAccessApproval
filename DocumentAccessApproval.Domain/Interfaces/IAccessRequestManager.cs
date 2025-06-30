using DocumentAccessApproval.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentAccessApproval.Domain.Interfaces
{
    public interface IAccessRequestManager
    {
        Task<Guid> CreateAccessRequestAsync(AccessRequest accessRequest);
        Task<AccessRequest> GetAccessRequestAsync(Guid id);
        Task<IEnumerable<AccessRequest>> GetAccessRequestsAsync();
        Task UpdateAccessRequestDecisionAsync(Guid id, string username, Decision decision);
    }
}

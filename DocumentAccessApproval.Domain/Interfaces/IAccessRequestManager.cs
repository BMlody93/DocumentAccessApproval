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
        AccessRequest GetAccessRequest(Guid Id);
        IEnumerable<AccessRequest> GetAccessRequests();
        Guid CreateAccessRequest(AccessRequest accessRequest);
        void UpdateAccessRequestDecision(Guid id, string username, Decision decision);
    }
}

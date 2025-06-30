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
    public class AccessRequestManager : IAccessRequestManager
    {
        public Guid CreateAccessRequest(AccessRequest accessRequest)
        {
            using (var dbContext = new DatabaseContext())
            {
                if(accessRequest.Id == Guid.Empty) 
                    accessRequest.Id = Guid.NewGuid();

                var user = dbContext.Users.FirstOrDefault(u=>u.Username == accessRequest.User.Username);
                if(user == null)
                    throw new Exception("User does not exist");

                accessRequest.UserId = user.Id;

                var document = dbContext.Documents.FirstOrDefault(d => d.Id == accessRequest.DocumentId);
                if(document == null)
                    throw new Exception("Document does not exist");

                accessRequest.DocumentId = document.Id;

                accessRequest.Decision = new Decision()
                {
                    Id = Guid.NewGuid(),
                    Status = Status.Awaiting,
                    AccessRequestId = accessRequest.Id
                };

                dbContext.AccessRequests.Add(accessRequest);
                dbContext.SaveChanges();
            }

            return accessRequest.Id;
        }

        public AccessRequest GetAccessRequest(Guid id)
        {
            using (var dbContext = new DatabaseContext())
            {
               var accessRequest = dbContext.AccessRequests.FirstOrDefault(ar=>ar.Id == id);
               return accessRequest;
            }
        }

        public IEnumerable<AccessRequest> GetAccessRequests()
        {
            using (var dbContext = new DatabaseContext())
            {
                var accessRequests = dbContext.AccessRequests.ToList();
                return accessRequests;
            }
        }

        public void UpdateAccessRequestDecision(Guid id, string username, Decision decision)
        {
            using (var dbContext = new DatabaseContext())
            {
                var accessRequest = dbContext.AccessRequests.FirstOrDefault(ar => ar.Id == id);
                if (accessRequest == null)
                    throw new Exception("Cannot find request");

                var user = dbContext.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                    throw new Exception("User does not exist");

                if (user.UserType != UserType.Approver)
                    throw new Exception("This user cannot aprove requests");

                accessRequest.Decision.MadeByUser = user;
                accessRequest.Decision.Status = decision.Status;

                dbContext.AccessRequests.Update(accessRequest);
                dbContext.SaveChanges();
            }
        }
    }
}

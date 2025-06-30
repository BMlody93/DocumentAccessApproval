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
    public class AccessRequestManager : IAccessRequestManager
    {
        private readonly DatabaseContext _dbContext;

        public AccessRequestManager(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> CreateAccessRequestAsync(AccessRequest accessRequest)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(accessRequest);

                if (accessRequest.Id == Guid.Empty)
                    accessRequest.Id = Guid.NewGuid();

                var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == accessRequest.User.Username);
                if (user == null)
                    throw new Exception("User does not exist");

                accessRequest.UserId = user.Id;
                accessRequest.User = null; // do not create new user

                var document = await _dbContext.Documents.SingleOrDefaultAsync(d => d.Id == accessRequest.DocumentId);
                if (document == null)
                    throw new Exception("Document does not exist");

                accessRequest.DocumentId = document.Id;

                accessRequest.Decision = new Decision()
                {
                    Id = Guid.NewGuid(),
                    Status = Status.Awaiting,
                    AccessRequestId = accessRequest.Id
                };

                await _dbContext.AccessRequests.AddAsync(accessRequest);
                await _dbContext.SaveChangesAsync();


                return accessRequest.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<AccessRequest> GetAccessRequestAsync(Guid id)
        {
            try
            {
                var accessRequest = await _dbContext.AccessRequests.SingleOrDefaultAsync(ar => ar.Id == id);

                if (accessRequest == null)
                    throw new Exception("Request does not exist");

                return accessRequest;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<AccessRequest>> GetAccessRequestsAsync()
        {
            try
            {
                var accessRequests = await _dbContext.AccessRequests.ToListAsync();
                return accessRequests;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateAccessRequestDecisionAsync(Guid id, string username, Decision decision)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(username);
                ArgumentNullException.ThrowIfNull(decision);

                if (decision.Status == Status.Denied && string.IsNullOrEmpty(decision.Reason))
                    throw new Exception("Have to give reason for rejecting request");

                var accessRequest = await _dbContext.AccessRequests.SingleOrDefaultAsync(ar => ar.Id == id);
                if (accessRequest == null)
                    throw new Exception("Cannot find request");

                var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username);
                if (user == null)
                    throw new Exception("User does not exist");

                if (user.UserType != UserType.Approver)
                    throw new Exception("This user cannot aprove requests");

                accessRequest.Decision.MadeByUserId = user.Id;
                accessRequest.Decision.Status = decision.Status;
                accessRequest.Decision.Reason = decision.Reason;

                _dbContext.AccessRequests.Update(accessRequest);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}

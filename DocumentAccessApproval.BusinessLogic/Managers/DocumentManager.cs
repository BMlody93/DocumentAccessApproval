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
    public class DocumentManager : IDocumentManager
    {
        private readonly DatabaseContext _dbContext;

        public DocumentManager(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task EditDocumentAsync(string username, Document document)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(username);
                ArgumentNullException.ThrowIfNull(document);
                var accessRequest = await _dbContext.AccessRequests.SingleOrDefaultAsync(ar =>
                        ar.User.Username == username &&
                        ar.AccessType == AccessType.Read &&
                        ar.Decision.Status == Status.Approved &&
                        ar.Document.Id == document.Id);

                if (accessRequest == null)
                    throw new Exception("User does not have aproved request for editing this document");

                accessRequest.Document.Content = document.Content;
                accessRequest.Document.Name = document.Name;

                _dbContext.AccessRequests.Update(accessRequest);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Document> GetDocumentAsync(Guid documentId, string username)
        {
            try
            {
                ArgumentException.ThrowIfNullOrEmpty(username);
                var accessRequest = await _dbContext.AccessRequests.SingleOrDefaultAsync(ar =>
                        ar.User.Username == username &&
                        ar.AccessType == AccessType.Read &&
                        ar.Decision.Status == Status.Approved &&
                        ar.Document.Id == documentId);

                if (accessRequest == null)
                    throw new Exception("User does not have aproved request for reading this document");

                return accessRequest.Document;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Document>> GetDocumentsAsync()
        {
            try
            {
                var documents = await _dbContext.Documents.ToListAsync();
                return documents;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

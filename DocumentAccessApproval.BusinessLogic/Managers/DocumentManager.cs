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
    public class DocumentManager : IDocumentManager
    {
        public void EditDocument(string username, Document document)
        {
            using (var dbContext = new DatabaseContext())
            {
                var accessRequest = dbContext.AccessRequests.FirstOrDefault(ar =>
                    ar.User.Username == username &&
                    ar.AccessType == AccessType.Read &&
                    ar.Decision.Status == Status.Approved &&
                    ar.Document.Id == document.Id);

                if (accessRequest == null)
                    throw new Exception("User does not have aproved request for editing this document");
                
                accessRequest.Document.Content = document.Content;
                accessRequest.Document.Name = document.Name;

                dbContext.AccessRequests.Update(accessRequest);
                dbContext.SaveChanges();
            }
        }

        public Document GetDocument(Guid documentId, string username)
        {
            using (var dbContext = new DatabaseContext())
            {
                var accessRequest = dbContext.AccessRequests.FirstOrDefault(ar=>
                    ar.User.Username == username && 
                    ar.AccessType == AccessType.Read && 
                    ar.Decision.Status == Status.Approved &&
                    ar.Document.Id == documentId);

                if (accessRequest == null)
                    throw new Exception("User does not have aproved request for reading this document");

                return accessRequest.Document;
            }

        }

        public IEnumerable<Document> GetDocuments()
        {
            using (var dbContext = new DatabaseContext())
            {
                var documents = dbContext.Documents.ToList();
                return documents;
            }
        }
    }
}

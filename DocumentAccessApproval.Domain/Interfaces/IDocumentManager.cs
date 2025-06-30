using DocumentAccessApproval.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentAccessApproval.Domain.Interfaces
{
    public interface IDocumentManager
    {
        Task EditDocumentAsync(string username, Document document);
        Task<Document> GetDocumentAsync(Guid documentId, string username);
        Task<IEnumerable<Document>> GetDocumentsAsync();
    }
}

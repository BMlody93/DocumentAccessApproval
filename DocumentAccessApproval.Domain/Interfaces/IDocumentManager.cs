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
        Document GetDocument(Guid docuemntId, string username);
        IEnumerable<Document> GetDocuments();
        void EditDocument(string username, Document document);
    }
}

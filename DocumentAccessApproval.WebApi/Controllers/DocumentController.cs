using DocumentAccessApproval.BusinessLogic.Managers;
using DocumentAccessApproval.Domain.Interfaces;
using DocumentAccessApproval.Domain.Models;
using DocumentAccessApproval.WebApi.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DocumentAccessApproval.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        public IDocumentManager _documentManager { get; set; }
        public DocumentController() {
            _documentManager = new DocumentManager();
        }

        // GET: api/<DocumentController>
        [HttpGet]
        [Authorize]
        public IEnumerable<DocumentDto> Get()
        {
            var documentsDto = _documentManager.GetDocuments()
                .Select(d => new DocumentDto() 
                { 
                    DocumetId = d.Id, 
                    Name = d.Name 
                });

            return documentsDto;
        }

        // GET api/<DocumentController>/5
        [HttpGet("{id}")]
        [Authorize]
        public DocumentDto Get(Guid documentId)
        {
            var username = User.Identity.Name;
            var document = _documentManager.GetDocument(documentId, username);

            var documentDto = new DocumentDto()
            {
                DocumetId = document.Id,
                Name = document.Name,
                Content = document.Content
            };

            return documentDto;
        }

        // PUT api/<DocumentController>/5
        [HttpPut("{id}")]
        [Authorize]
        public void Put(Guid accessRequestId, [FromBody] EditDocumentDto documentDto)
        {
            var username = User.Identity.Name;
            var document = new Document()
            {
                Name = documentDto.Name,
                Content = documentDto.Content
            };

            _documentManager.EditDocument(username, document);
        }
    }
}

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
    [Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentManager _documentManager;
        public DocumentController(IDocumentManager documentManager)
        {
            _documentManager = documentManager;
        }

        /// <summary>
        /// Get all documents (without content)
        /// </summary>
        /// <returns></returns>
        // GET: api/<DocumentController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> Get()
        {
            var documentsDto = (await _documentManager.GetDocumentsAsync())
                .Select(d => new DocumentDto() 
                {
                    DocumentId = d.Id, 
                    Name = d.Name 
                });

            return Ok(documentsDto);
        }

        /// <summary>
        /// Get document with specific id
        /// </summary>
        /// <param name="id">Id of document</param>
        /// <returns></returns>
        // GET api/<DocumentController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentDto>> Get(Guid id)
        {
            var username = User.Identity.Name;
            var document = await _documentManager.GetDocumentAsync(id, username);

            if (document == null)
                return NotFound();

            var documentDto = new DocumentDto()
            {
                DocumentId = document.Id,
                Name = document.Name,
                Content = document.Content
            };

            return Ok(documentDto);
        }

        /// <summary>
        /// Edit document with specific id
        /// </summary>
        /// <param name="id">id of document</param>
        /// <param name="documentDto">object containing all parameters needed to edit document which include:
        /// - name of document
        /// - content of document</param>
        // PUT api/<DocumentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] EditDocumentDto documentDto)
        {
            var username = User.Identity.Name;
            var document = new Document()
            {
                Id = id,
                Name = documentDto.Name,
                Content = documentDto.Content
            };

            await _documentManager.EditDocumentAsync(username, document);
            return NoContent();
        }
    }
}

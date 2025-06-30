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
    public class RequestAccessController : ControllerBase
    {
        private readonly IAccessRequestManager _accessRequestManager;
        public RequestAccessController(IAccessRequestManager accessRequestManager)
        {
            _accessRequestManager = accessRequestManager;
        }

        /// <summary>
        /// Get all AccessRequests
        /// </summary>
        /// <returns></returns>
        // GET: api/<RequestAccessController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessRequestDto>>> Get()
        {
            var accessRequests = await _accessRequestManager.GetAccessRequestsAsync();
            var accessRequestsDto = accessRequests.Select(ar => new AccessRequestDto()
            {
                Id = ar.Id,
                Username = ar.User?.Username,
                DocumentName = ar.Document?.Name,
                AccessReason = ar.AccessReason,
                AccessType = (int)ar.AccessType,
                DecisionStatus = (int)ar.Decision.Status
            });

            return Ok(accessRequestsDto);
        }


        /// <summary>
        /// Get access request with specified id
        /// </summary>
        /// <param name="id">id of access request</param>
        /// <returns></returns>
        // GET api/<RequestAccessController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccessRequestDto>> Get(Guid id)
        {
            var accessRequest = await _accessRequestManager.GetAccessRequestAsync(id);

            if (accessRequest == null)
                return NotFound();

            var accessRequestDto = new AccessRequestDto()
            {
                Id = accessRequest.Id,
                Username = accessRequest.User.Username,
                DocumentName = accessRequest.Document.Name,
                AccessReason = accessRequest.AccessReason,
                AccessType = (int)accessRequest.AccessType,
                DecisionStatus = (int)accessRequest.Decision.Status
            };

            return Ok(accessRequestDto);
        }

        /// <summary>
        /// Create access request
        /// </summary>
        /// <param name="accessRequestDto">Object containing all needed parameters for creating accessRequest which include:
        /// - document id of document you want to access
        /// - access type to specify if you want to read or edit document
        /// - reason for access
        /// </param>
        // POST api/<RequestAccessController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAccessRequestDto accessRequestDto)
        {
            if (accessRequestDto == null)
                return BadRequest("Invalid request data.");

            var accessRequest = new AccessRequest()
            {
                Id = Guid.NewGuid(),
                User = new User() { Username = User.Identity?.Name ?? string.Empty },
                DocumentId = accessRequestDto.DocumentId,
                AccessType = (AccessType)accessRequestDto.AccessType,
                AccessReason = accessRequestDto.AccessReason,
            };

            await _accessRequestManager.CreateAccessRequestAsync(accessRequest);
            return CreatedAtAction(nameof(Get), new { id = accessRequest.Id }, null);
        }

        /// <summary>
        /// Change decision of accessRequest
        /// </summary>
        /// <param name="id"> id of accessRequest</param>
        /// <param name="decisionDto">object containing all parameters of changed decision which include:
        /// - decision status
        /// - reason for decision (required if rejecting request)</param>
        // PATCH api/<RequestAccessController>/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateAccessRequestDecisionDto decisionDto)
        {
            if (decisionDto == null)
                return BadRequest("Invalid request data.");

            var username = User.Identity?.Name ?? string.Empty;

            var decision = new Decision()
            {
                Status = (Status)decisionDto.DecisionStatus,
                Reason = decisionDto.Reason
            };

            await _accessRequestManager.UpdateAccessRequestDecisionAsync(id, username, decision);
            return NoContent();
        }
    }
}

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
    public class RequestAccessController : ControllerBase
    {
        public IAccessRequestManager _accessRequestManager { get; set; }
        public RequestAccessController()
        {
            _accessRequestManager = new AccessRequestManager();
        }

        // GET: api/<RequestAccessController>
        [HttpGet]
        [Authorize]
        public IEnumerable<AccessRequestDto> Get()
        {
            var accessRequestsDto = _accessRequestManager.GetAccessRequests()
                .Select(ar => new AccessRequestDto() {
                Id = ar.Id,
                Username = ar.User.Username,
                DocumentName = ar.Document.Name,
                AccessReason = ar.AccessReason,
                AccessType = (int)ar.AccessType,
                DecisionStatus = (int)ar.Decision.Status
            });

            return accessRequestsDto;
        }

        // GET api/<RequestAccessController>/5
        [HttpGet("{id}")]
        [Authorize]
        public AccessRequestDto Get(Guid id)
        {
            var accessRequest = _accessRequestManager.GetAccessRequest(id);
            var accessRequestDto = new AccessRequestDto()
            {
                Id = accessRequest.Id,
                Username = accessRequest.User.Username,
                DocumentName = accessRequest.Document.Name,
                AccessReason = accessRequest.AccessReason,
                AccessType = (int)accessRequest.AccessType,
                DecisionStatus = (int)accessRequest.Decision.Status
            };

            return accessRequestDto;
        }

        // POST api/<RequestAccessController>
        [HttpPost]
        [Authorize]
        public void Post([FromBody] CreateAccessRequestDto accessRequestDto)
        {
            var accessRequest = new AccessRequest()
            {
                Id = Guid.NewGuid(),
                User = new User()
                {
                    Username = User.Identity.Name
                },
                DocumentId = accessRequestDto.DocumentId,
                AccessType = (AccessType)accessRequestDto.AccessType,
                AccessReason = accessRequestDto.AccessReason,
            };

            _accessRequestManager.CreateAccessRequest(accessRequest);
        }

        // PATCH api/<RequestAccessController>/5
        [HttpPatch("{id}")]
        [Authorize]
        public void Put(Guid id, [FromBody] UpdateAccessRequestDecisionDto decisionDto)
        {
            var username = User.Identity.Name;
            var decision = new Decision()
            {
                Status = (Status)decisionDto.DecisionStatus
            };

            _accessRequestManager.UpdateAccessRequestDecision(id, username, decision);
        }
    }
}

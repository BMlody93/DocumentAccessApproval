using System.ComponentModel.DataAnnotations;

namespace DocumentAccessApproval.WebApi.DTOs
{
    /// <summary>
    /// Dto object used to create AccessRequest
    /// </summary>
    public class CreateAccessRequestDto
    {
        /// <summary>
        /// Id of document which user want to access
        /// </summary>
        [Required]
        public Guid DocumentId { get; set; }
        /// <summary>
        /// type of access user want to have to document (read/write)
        /// </summary>
        [Required]
        public int AccessType { get; set; }
        /// <summary>
        /// Reason for requesting access
        /// </summary>
        [Required]
        public required string AccessReason { get; set; }
    }
}

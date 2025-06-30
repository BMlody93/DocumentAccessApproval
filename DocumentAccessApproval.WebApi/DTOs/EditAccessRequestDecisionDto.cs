using System.ComponentModel.DataAnnotations;

namespace DocumentAccessApproval.WebApi.DTOs
{
    /// <summary>
    /// Dto used to update decision of existing request
    /// </summary>
    public class UpdateAccessRequestDecisionDto
    {
        /// <summary>
        /// New status of decision (approved/rejected/awaiting)
        /// </summary>
        [Required]
        public int DecisionStatus { get; set; }
        /// <summary>
        /// Reason for decison, required if rejecting
        /// </summary>
        public string Reason { get; set; }
    }
}

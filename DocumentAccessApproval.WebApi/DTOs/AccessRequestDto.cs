namespace DocumentAccessApproval.WebApi.DTOs
{
    /// <summary>
    /// Dto object for returning AccessRequest
    /// </summary>
    public class AccessRequestDto
    {
        /// <summary>
        /// Id of AccessRequest
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Username of user who created request
        /// </summary>
        public  string Username { get; set; }
        /// <summary>
        /// Name of document for which request was made
        /// </summary>
        public string DocumentName { get; set; }
        /// <summary>
        /// Status of request
        /// </summary>
        public int DecisionStatus { get; set; }
        /// <summary>
        /// Access type (read/edit)
        /// </summary>
        public int AccessType { get; set; }
        /// <summary>
        /// reason for requesting access
        /// </summary>
        public  string AccessReason { get; set; }
    }
}

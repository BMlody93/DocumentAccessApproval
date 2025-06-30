namespace DocumentAccessApproval.WebApi.DTOs
{
    public class CreateAccessRequestDto
    {
        public Guid DocumentId { get; set; }
        public int AccessType { get; set; }
        public required string AccessReason { get; set; }
    }
}

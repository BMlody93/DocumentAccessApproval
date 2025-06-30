namespace DocumentAccessApproval.WebApi.DTOs
{
    public class AccessRequestDto
    {
        public Guid Id { get; set; }
        public  string Username { get; set; }
        public string DocumentName { get; set; }
        public int DecisionStatus { get; set; }
        public int AccessType { get; set; }
        public  string AccessReason { get; set; }
    }
}

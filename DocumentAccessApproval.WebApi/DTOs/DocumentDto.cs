namespace DocumentAccessApproval.WebApi.DTOs
{
    public class DocumentDto
    {
        public Guid DocumetId { get; set; }
        public string Name { get; set; }
        public Byte[] Content { get; set; }
    }
}

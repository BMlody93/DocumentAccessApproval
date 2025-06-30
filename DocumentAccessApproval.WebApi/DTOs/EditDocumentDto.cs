namespace DocumentAccessApproval.WebApi.DTOs
{
    public class EditDocumentDto
    {
        public required string Name { get; set; }
        public required Byte[] Content { get; set; }
    }
}

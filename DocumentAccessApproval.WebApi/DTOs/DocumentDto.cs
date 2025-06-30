namespace DocumentAccessApproval.WebApi.DTOs
{
    /// <summary>
    /// Dto used to return document
    /// </summary>
    public class DocumentDto
    {
        /// <summary>
        /// id of document
        /// </summary>
        public Guid DocumentId { get; set; }
        /// <summary>
        /// Name of document
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Content of document
        /// </summary>
        public Byte[] Content { get; set; }
    }
}

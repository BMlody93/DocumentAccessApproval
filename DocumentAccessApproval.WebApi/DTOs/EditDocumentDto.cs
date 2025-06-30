using System.ComponentModel.DataAnnotations;

namespace DocumentAccessApproval.WebApi.DTOs
{
    /// <summary>
    /// DTO used to edit existing document
    /// </summary>
    public class EditDocumentDto
    {
        /// <summary>
        /// New name of document
        /// </summary>
        [Required]
        public required string Name { get; set; }
        /// <summary>
        /// New content of document
        /// </summary>
        [Required]
        public required Byte[] Content { get; set; }
    }
}

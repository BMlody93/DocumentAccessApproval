using System.ComponentModel.DataAnnotations;

namespace DocumentAccessApproval.WebApi.DTOs
{
    /// <summary>
    /// DTO used to login user
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// Username of the user 
        /// </summary>
        [Required]
        public required string Username { get; set; }
        /// <summary>
        /// Password of the user
        /// </summary>
        [Required]
        public required string Password { get; set; }
    }
}

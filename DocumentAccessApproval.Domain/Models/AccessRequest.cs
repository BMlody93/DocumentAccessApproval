using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DocumentAccessApproval.Domain.Models
{
    public class AccessRequest
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid DocumentId { get; set; } 
        public virtual Document Document { get; set; }
        public virtual Decision Decision { get; set; }
        public AccessType AccessType { get; set; }
        public string AccessReason { get; set; }
        
    }

    public enum AccessType
    {
        Read,
        Edit
    }
}

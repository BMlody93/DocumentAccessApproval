using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentAccessApproval.Domain.Models
{
    public class Decision
    {
        [Key]
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public string? Reason { get; set; }

        public Guid MadeByUserId { get; set; }
        public virtual User MadeByUser { get; set; }

        public Guid AccessRequestId { get; set; }
        public virtual AccessRequest Request { get; set; }
    }

    public enum Status
    {
        Awaiting,
        Approved,
        Denied
    }
}

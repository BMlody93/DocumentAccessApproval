using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentAccessApproval.Domain.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public UserType UserType { get; set; }

        public virtual List<AccessRequest> Requests { get; set; }
        public virtual List<Decision> Decisions { get; set; }


        [Required]
        public string Username { get; set; }
        
    }

    public enum UserType
    {
        Common,
        Approver,
        Admin
    }
}

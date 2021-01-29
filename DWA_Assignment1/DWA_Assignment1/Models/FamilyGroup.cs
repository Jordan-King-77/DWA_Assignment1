using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DWA_Assignment1.Models
{
    public class FamilyGroup
    {
        [Key]
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public virtual ApplicationUser Parent { get; set; }
        public virtual List<ApplicationUser> Swimmers { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
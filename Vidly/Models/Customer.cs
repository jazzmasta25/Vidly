using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Vidly.Controllers;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
        
        public bool IsSubscribedToNewsLetter { get; set; }

        
        [Display(Name = "Membership Type")]
        [ForeignKey("MembershipType")]
        public int MembershipTypeId {get; set; }  //MUST BE TYPE BYTE ! ! !! !
        public MembershipType MembershipType { get; set; }


    }
}
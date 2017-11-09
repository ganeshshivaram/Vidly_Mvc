using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Validators;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter customer's name")]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        [Required]
        public byte MembershipTypeId { get; set; }

        [Display(Name = "Date of Birth")]
        [Required]
        [Min18YearsForAMember]
        public DateTime? BirthDate { get; set; }
    }
}
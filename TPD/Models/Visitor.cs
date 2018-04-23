using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPD.Models
{

    public class Visitor
    {
        public int Id { get; set; }
        [Required]
        public int TicketId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(50, ErrorMessage = "Email addresses cannot be longer than 50 characters")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "Last name cannot be longer than 10 characters")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public Ticket Ticket { get; set; }
    }
}

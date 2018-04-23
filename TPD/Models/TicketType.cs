using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPD.Models
{

    public class TicketType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Ticket names cannot be longer than 20 characters")]
        [Display(Name = "Ticket Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Ticket descriptions cannot be longer than 100 characters")]
        [Display(Name = "Ticket Type Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Ticket Price")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Number of Uses")]
        public int Uses { get; set; }
    }
}
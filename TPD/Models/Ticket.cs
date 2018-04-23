using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPD.Models
{

    public class Ticket
    {
        public int Id { get; set; }
        [Required]
        public int TicketTypeId { get; set; }
        [Required]
        [ForeignKey("Visitor")]
        public int VisitorId { get; set; }
        public int DailyReportId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ticket Date Purchased")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatePurchased { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Ticket Date Redeemed")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateRedeemed { get; set; }

        [Required]
        public TicketType TicketType { get; set; }
        [Required]
        
        public Visitor Visitor { get; set; }
        public DailyReport DailyReport { get; set; }
    }
}
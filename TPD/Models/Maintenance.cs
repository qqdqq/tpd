using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPD.Models
{

    public class Maintenance
    {
        public int Id { get; set; }
        [Required]
        public int AttractionId { get; set; }
        public int DailyReportId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Maintenance Descriptions cannot be longer than 500 characters")]
        [Display(Name = "Maintenance Request Description")]
        public string Description { get; set; }

        public string CurrentStatus { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date Recieved")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateRecieved { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Resolved")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateResolved { get; set; }

        [Required]
        [Display(Name = "Maintenance Cost")]
        public decimal Cost { get; set; }

        
        public Attraction Attraction { get; set; }
        public DailyReport DailyReport { get; set; }

    }
}

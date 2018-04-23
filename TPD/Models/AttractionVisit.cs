using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPD.Models {

    public class AttractionVisit
    {
        public int Id { get; set; }
        [Required]
        public int AttractionId { get; set; }
        [Required]
        public int DailyReportId { get; set; }

        [Required]
        [Display(Name = "Visit Date")]
        public DateTime VisitDate { get; set; }

        public Attraction Attraction { get; set; }
        public DailyReport DailyReport { get; set; }
    }
}

   




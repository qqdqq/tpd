using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPD.Models
{

    public class VendorSale
    {
        public int Id { get; set; }
        [Required]
        public int VendorId { get; set; }
        public int DailyReportId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Vendor Report Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReportDate { get; set; }

        [Required]
        [Display(Name = "Vendor Total Sales")]
        public decimal TotalSales { get; set; }

        [Display(Name = "Vendor Sales Goal")]
        public decimal SalesGoal { get; set; }

        [Required]
        public Vendor Vendor { get; set; }
        public DailyReport DailyReport { get; set; }

    }
}
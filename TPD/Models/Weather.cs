using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPD.Models
{

    public class Weather
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public bool Rainout { get; set; }

        [Required]
        [Display(Name = "Average Temperature")]
        public decimal Temperature { get; set; }

        [Display(Name = "Inches of Rain")]
        public decimal InchesOfRain { get; set; }

    }
}
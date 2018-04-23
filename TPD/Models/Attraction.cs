using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPD.Models {

    public class Attraction
    {
        public int Id { get; set; }
        [Required]
        public int ParkInfoId { get; set; }
        [Required]
        public int AttractionTypeId { get; set; }
        [Required]
        public int LocationId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The attraction name cannot be longer than 50 characters")]
        [Display(Name = "Attraction Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        [Display(Name = "Attraction Description")]
        public string Description { get; set; }

        [Required]
        public Location Locaiton { get; set; }
        [Required]
        public AttractionType AttractionType { get; set; }
        [Required]
        public ParkInfo ParkInfo { get; set; }

        public ICollection<Maintenance> MaintenanceList { get; set; }
        public ICollection<AttractionVisit> AttractionVisitorsList { get; set; }

    }
}
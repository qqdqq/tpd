using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPD.Models {

    public class AttractionType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "An attraction type name cannot be longer than 50 characters")]
        [Display(Name = "Attraction Type Name")]
        public string Name { get; set; }
    }
}
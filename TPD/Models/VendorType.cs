using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPD.Models
{

    public class VendorType
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "A vendor type name cannot be longer than 50 characters")]
        [Display(Name = "Vendor Type Name")]
        public string Name { get; set; }
    }
}
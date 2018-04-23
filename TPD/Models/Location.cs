using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPD.Models {

    public class Location
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "A location name cannot be longer than 50 characters")]
        [Display(Name = "Location Name")]
        public string Name { get; set; }

        /*
        public ICollection<Attraction> AttractionLocations { get; set; }
        public ICollection<Vendor> VendorLocations { get; set; }
        public ICollection<Department> DepartmentLocations { get; set; }
        */
    }

}
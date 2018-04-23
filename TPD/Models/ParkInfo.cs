using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPD.Models {

    public class ParkInfo
    {
        public int Id { get; set; }
        [Required]
        public int ParkHoursId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The name of the theme park cannot be longer than 100 characters")]
        [Display(Name = "Theme Park Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Country cannot be longer than 3 characters. Use the abbreviation (i.e. USA)")]
        [Display(Name = "Park Country")]
        public string Country { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessage = "States cannot be longer than 2 characters")]
        [Display(Name = "Park State")]
        public string State { get; set; }

        [StringLength(20, ErrorMessage = "City names cannot be longer than 20 characters.")]
        [Display(Name = "Park City")]
        public string City { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Zip codes cannot be longer than 5 characters")]
        [Display(Name = "Park ZIP Code")]
        public string ZipCode { get; set; }

        [Required]
        [StringLength(5, ErrorMessage = "Street numbers cannot be longer than 5 characters")]
        [Display(Name = "Street Number")]
        public string StreetNumber { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Street names cannot be longer than 50 characters")]
        [Display(Name = "Street Name")]
        public string StreetName { get; set; }

        public string FullAddress
        {
            get { return StreetNumber + " " + StreetName; }
        }

        public ParkHour ParkHours;
        public ICollection<Attraction> Attractions;
        public ICollection<Department> Departments;
        public ICollection<Vendor> Vendors;



    }
}
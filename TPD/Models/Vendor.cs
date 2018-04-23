using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPD.Models
{

    public class Vendor
    {
        public int Id { get; set; }
        [Required]
        public int ParkInfoId { get; set; }
        [Required]
        public int VendorTypeId { get; set; }
        [Required]
        public int LocationId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Department name cannot be longer than 50 characters")]
        [Display(Name = "Vendor Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Department name cannot be longer than 500 characters")]
        [Display(Name = "Vendor Description")]
        public string Description { get; set; }

        [Required]
        public ParkInfo ParkInfo { get; set; }
        [Required]
        public Location Location { get; set; }
        [Required]
        public VendorType VendorType { get; set; }

        public ICollection<VendorSale> VendorSaleList { get; set; }
    }
}
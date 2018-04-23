using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPD.Models
{

    public class Department
    {
        public int Id { get; set; }
        [Required]
        public int ParkInfoId { get; set; }
        [Required]
        public int LocationId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Department name cannot be longer than 50 characters")]
        [Display(Name = "Department Name")]
        public string Name { get; set; }

        [Required]
        public ParkInfo ParkInfo { get; set; }
        [Required]
        public Location Location { get; set; }
        [Required]
        public ICollection<Employee> EmployeeList { get; set; }
    }
}

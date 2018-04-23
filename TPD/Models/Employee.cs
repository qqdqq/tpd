using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TPD.Models {

    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(1, MinimumLength = 1, ErrorMessage = "Middle Initial cannot be longer than 1 characters")]
        [Display(Name = "Middle Initial")]
        public string MiddleInitial { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Job Titles cannot be longer than 50 characters")]
        [Display(Name = "Job Title")]
        public string Title { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }

        public string FullName
        {
            get { return LastName + ", " + FirstName + ", " + MiddleInitial; }
        }

        [Required]
        public Department Department { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using TPD.Data;

namespace TPD.Models
{
    public class ParkHour
    {
        public int Id { get; set; }
        [Required]
        public int ParkInfoId { get; set; }

        [Required]
        public string FirstDayOpen { get; set; }
        [Required]
        public string LastDayOpen { get; set; }
        [Required]
        public string WorkingHours { get; set; }

        [Required]
        public ParkInfo ParkInfo { get; set; }
    }
}

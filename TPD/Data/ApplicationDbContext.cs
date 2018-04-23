using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TPD.Models;

namespace TPD.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Attraction> Attractions { get; set; }
        public DbSet<AttractionType> AttractionTypes { get; set; }
        public DbSet<AttractionVisit> AttractionVisits { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Visitor> Visitors { get; set; }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VendorType> VendorTypes { get; set; }
        public DbSet<VendorSale> VendorSales { get; set; }

        public DbSet<DailyReport> DailyReports { get; set; }
        public DbSet<ParkInfo> ParkInfo { get; set; }
        public DbSet<ParkHour> ParkHours { get; set; }
        public DbSet<Location> Locaitons { get; set; }
        public DbSet<Weather> Weathers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Weather>().ToTable("Weather");
            builder.Entity<ParkInfo>().ToTable("ParkInfo");

            
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}

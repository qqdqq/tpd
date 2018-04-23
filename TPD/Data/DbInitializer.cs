using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TPD.Models;
using TPD.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TPD.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPW)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var adminId = await EnsureUser(serviceProvider, testUserPW, "admin@TPD.com");
                await EnsureRole(serviceProvider, adminId, Constants.AttractionAdministratorsRole);

                /*
                var mid = await EnsureUser(serviceProvider, testUserPW, "manager@TPD.com");
                await EnsureRole(serviceProvider, mid, Constants.AttractionManagerRole);
                */
                InitializeDb(context, adminId);
            }
        }

        private async static Task<string> EnsureUser(IServiceProvider serviceProvider, string testUserPW, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if(user == null)
            {
                user = new ApplicationUser { UserName = UserName };
                await userManager.CreateAsync(user, testUserPW);
                
            }
            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            if(!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByIdAsync(uid);

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        public static void InitializeDb(ApplicationDbContext context, string adminId)
        {
            if (context.ParkInfo.Any())
            {
                return;
            }

            var pinfo = new ParkInfo[]
{
                new ParkInfo
                {
                    Name = "Houston Theme Park",
                    Country = "USA",
                    State = "TX",
                    City = "Houston",
                    ZipCode = "77042",
                    StreetNumber = "11223",
                    StreetName = "Grant Rd"
                },
};
            foreach (ParkInfo pi in pinfo) { context.ParkInfo.Add(pi); }
            context.SaveChanges();

            var phours = new ParkHour[]
            {
                new ParkHour
                {
                    ParkInfoId = pinfo.Single(s => s.Name == "Houston Theme Park").Id,
                    FirstDayOpen = "Monday",
                    LastDayOpen = "Saturday",
                    WorkingHours = "8:00am - 10:00pm"
                }


            };

            foreach(ParkHour ph in phours)
            {
                var parkHoursInDatabase = context.ParkHours.Where(s => s.ParkInfo.Id == ph.ParkInfoId).SingleOrDefault();
                if(parkHoursInDatabase == null)
                {
                    context.ParkHours.Add(ph);
                }
            }

            context.SaveChanges();


            var weather = new Weather[]
            {
                new Weather
                {
                    Date = DateTime.Parse("2009-10-03") ,
                    Rainout = false,
                    Temperature = 70.0m,
                    InchesOfRain = 0.0m
                }
            };
            foreach (Weather w in weather) { context.Weathers.Add(w); }
            context.SaveChanges();


            var dreports = new DailyReport[]
            {
                new DailyReport
                {
                    WeatherId = weather.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id,
                    Date = DateTime.Parse("2009-10-03")
                }
            };

            foreach (DailyReport dr in dreports) { context.DailyReports.Add(dr); }

            context.SaveChanges();


            var locations = new Location[]
            {
                new Location {Name = "North"},
                new Location {Name = "East"},
                new Location {Name = "West"},
                new Location {Name = "South"},
                new Location {Name = "Center"}

            };
            foreach (Location l in locations) { context.Locaitons.Add(l); }
            context.SaveChanges();

            var atypes = new AttractionType[]
            {
                new AttractionType {Name = "Roller Coaster"},
                new AttractionType {Name = "Merry go Round"},
                new AttractionType {Name = "Farris Wheel"},
                new AttractionType {Name = "Haunted House"},
                new AttractionType {Name = "Water Slide"}

            };
            foreach (AttractionType at in atypes) { context.AttractionTypes.Add(at); }
            context.SaveChanges();

            var attractions = new Attraction[]
            {
                new Attraction
                {
                    ParkInfoId = pinfo.Single(s => s.Name == "Houston Theme Park").Id,
                    AttractionTypeId = atypes.Single( s => s.Name == "Haunted House").Id,
                    LocationId = locations.Single( s => s.Name == "North").Id,
                    Name = "Spooky House",
                    Description = "Boo!",
                },
                new Attraction
                {
                    ParkInfoId = pinfo.Single(s => s.Name == "Houston Theme Park").Id,
                    AttractionTypeId = atypes.Single( s => s.Name == "Roller Coaster").Id,
                    LocationId = locations.Single( s => s.Name == "East").Id,
                    Name = "Wild Ride",
                    Description = "Wee!",
                },
                new Attraction
                {
                    ParkInfoId = pinfo.Single(s => s.Name == "Houston Theme Park").Id,
                    AttractionTypeId = atypes.Single( s => s.Name == "Water Slide").Id,
                    LocationId = locations.Single( s => s.Name == "West").Id,
                    Name = "Water Snake",
                    Description = "Hiss!",
                },
                new Attraction
                {
                    ParkInfoId = pinfo.Single(s => s.Name == "Houston Theme Park").Id,
                    AttractionTypeId = atypes.Single( s => s.Name == "Farris Wheel").Id,
                    LocationId = locations.Single( s => s.Name == "South").Id,
                    Name = "Great View",
                    Description = "Ooh!",
                },
                new Attraction
                {
                    ParkInfoId = pinfo.Single(s => s.Name == "Houston Theme Park").Id,
                    AttractionTypeId = atypes.Single( s => s.Name == "Merry go Round").Id,
                    LocationId = locations.Single( s => s.Name == "Center").Id,
                    Name = "Spinner",
                    Description = "Woha!",
                }

            };
            foreach (Attraction a in attractions)
            {
                var attractionsInDatabase = context.Attractions.Where
                    (s => s.ParkInfo.Id == a.ParkInfoId).SingleOrDefault();
                if (attractionsInDatabase == null)context.Attractions.Add(a);
            }
            context.SaveChanges();

            var avisits = new AttractionVisit[]
            {
                new AttractionVisit {VisitDate = DateTime.Parse("2008-01-20"), AttractionId = attractions.Single(s => s.Name == "Spinner").Id, DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id },
                new AttractionVisit {VisitDate = DateTime.Parse("2008-01-20"), AttractionId = attractions.Single(s => s.Name == "Great View").Id, DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id },
                new AttractionVisit {VisitDate = DateTime.Parse("2008-01-20"), AttractionId = attractions.Single(s => s.Name == "Water Snake").Id, DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id },
                new AttractionVisit {VisitDate = DateTime.Parse("2008-01-20"), AttractionId = attractions.Single(s => s.Name == "Wild Ride").Id, DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id },
                new AttractionVisit {VisitDate = DateTime.Parse("2008-01-20"), AttractionId = attractions.Single(s => s.Name == "Spooky House").Id, DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id }

            };
            foreach (AttractionVisit av in avisits)
            {
                var attractionVisitsInDatabase = context.AttractionVisits.Where
                                                 (s => s.Attraction.Id == av.AttractionId &&
                                                  s.DailyReport.Id == av.DailyReportId).SingleOrDefault();
                if(attractionVisitsInDatabase == null)
                {
                    context.AttractionVisits.Add(av);
                }
                
            }
            context.SaveChanges();

            var mrequest = new Maintenance[]
            {
                new Maintenance
                {
                    AttractionId = 1,
                    Description = "Spooky House as a leeky pipe",
                    DateRecieved = DateTime.Parse("2009-10-03"),
                    DateResolved = DateTime.Parse("2009-10-03"),
                    CurrentStatus = "Still leaky",
                    Cost = 53.96m,
                    DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id

                }
            };
            foreach (Maintenance m in mrequest)
            {
                var maintenanceInDatabase = context.Maintenances.Where
                                                 (s => s.Attraction.Id == m.AttractionId &&
                                                  s.DailyReport.Id == m.DailyReportId).SingleOrDefault();
                if (maintenanceInDatabase == null)
                {
                    context.Maintenances.Add(m);
                }

            }
            context.SaveChanges();

            var departments = new Department[]
         {
                new Department
                {
                    ParkInfoId = pinfo.Single(s => s.Name == "Houston Theme Park").Id,
                    LocationId = locations.Single( s => s.Name == "Center").Id,
                    Name = "Management"
                },
                new Department
                {
                    ParkInfoId = pinfo.Single(s => s.Name == "Houston Theme Park").Id,
                    LocationId = locations.Single( s => s.Name == "West").Id,
                    Name = "Maintenance"
                },
                new Department
                {
                    ParkInfoId = pinfo.Single(s => s.Name == "Houston Theme Park").Id,
                    LocationId = locations.Single( s => s.Name == "Center").Id,
                    Name = "Ride Attendants"
                },
                new Department
                {
                    ParkInfoId = pinfo.Single(s => s.Name == "Houston Theme Park").Id,
                    LocationId = locations.Single( s => s.Name == "South").Id,
                    Name = "Vendor Employee"
                }

         };
            foreach (Department d in departments) {

                var departmentsInDatabase = context.Departments.Where
                    (s => s.ParkInfo.Id == d.ParkInfoId).SingleOrDefault();
                if (departmentsInDatabase == null) context.Departments.Add(d);
            }
            context.SaveChanges();


            var employees = new Employee[]
            {
                new Employee
                {
                    FirstName = "Juan",
                    LastName = "Garcia",
                    Title = "Ride Attendant",
                    DepartmentId = departments.Single(s => s.Name == "Ride Attendants").Id,
                    HireDate = DateTime.Parse("2008-04-15"),
                    Salary = 10.25m
                },
                new Employee
                {
                    FirstName = "John",
                    MiddleInitial = "H",
                    LastName = "Smith",
                    Title = "Ride Attendant",
                    DepartmentId = departments.Single(s => s.Name == "Ride Attendants").Id,
                    HireDate = DateTime.Parse("2008-06-16"),
                    Salary = 8.25m
                },
                new Employee
                {
                    FirstName = "Sally",
                    LastName = "Red",
                    Title = "Ride Attendant",
                    DepartmentId = departments.Single(s => s.Name == "Ride Attendants").Id,
                    HireDate = DateTime.Parse("2008-05-21"),
                    Salary = 8.25m
                },
                new Employee
                {
                    FirstName = "Nick",
                    LastName = "Rodger",
                    Title = "Maintenance Crew",
                    DepartmentId = departments.Single(s => s.Name == "Maintenance").Id,
                    HireDate = DateTime.Parse("2007-09-25"),
                    Salary = 18.75m
                },
                new Employee
                {
                    FirstName = "David",
                    LastName = "Valentino",
                    Title = "Maintenance Crew",
                    DepartmentId = departments.Single(s => s.Name == "Maintenance").Id,
                    HireDate = DateTime.Parse("2007-09-15"),
                    Salary = 18.75m
                },
                new Employee
                {
                    FirstName = "Leon",
                    LastName = "Nobody",
                    Title = "Vendor Cashier",
                    DepartmentId = departments.Single(s => s.Name == "Vendor Employee").Id,
                    HireDate = DateTime.Parse("2008-02-07"),
                    Salary = 10.25m
                },
                new Employee
                {
                    FirstName = "Ashely",
                    LastName = "Frutiz",
                    Title = "Manager",
                    DepartmentId = departments.Single(s => s.Name == "Management").Id,
                    HireDate = DateTime.Parse("2006-07-09"),
                    Salary = 27.85m
                },


            };
            foreach (Employee e in employees)
            {
                var employeeInDatabase = context.Employees.Where
                    (s => s.Department.Id == e.DepartmentId).SingleOrDefault();
                if(employeeInDatabase == null)
                    context.Employees.Add(e);
            }
            context.SaveChanges();

            var ttypes = new TicketType[]
            {
                new TicketType
                {
                    Name = "Basic",
                    Description = "Basic, one time use ticket",
                    Price = 5.00m,
                    Uses = 1
                },
                new TicketType
                {
                    Name = "30-Day",
                    Description = "30 day ticket, good for 30 uses",
                    Price = 50.00m,
                    Uses = 30
                },
                new TicketType
                {
                    Name = "Premium",
                    Description = "Premium ticket, good for 180 uses",
                    Price = 100.00m,
                    Uses = 180
                }
            };
            foreach (TicketType tt in ttypes) { context.TicketTypes.Add(tt); }
            context.SaveChanges();

            var visitors = new Visitor[]
           {
                new Visitor
                {
                    FirstName = "John",
                    LastName = "Hop",
                    Email = "jh@mail.com",
                    PhoneNumber = "1234567890",
                    DateOfBirth = DateTime.Parse("1988-04-15"),
                },
                new Visitor
                {
                    FirstName = "John",
                    LastName = "Hope",
                    Email = "jh@mail.com",
                    PhoneNumber = "1234567890",
                    DateOfBirth = DateTime.Parse("1988-04-15"),
                },
                new Visitor
                {
                    FirstName = "John",
                    LastName = "Hopee",
                    Email = "jh@mail.com",
                    PhoneNumber = "1234567890",
                    DateOfBirth = DateTime.Parse("1988-04-15"),
                },
                new Visitor
                {
                    FirstName = "John",
                    LastName = "Hopeee",
                    Email = "jh@mail.com",
                    PhoneNumber = "1234567890",
                    DateOfBirth = DateTime.Parse("1988-04-15"),
                },
                new Visitor
                {
                    FirstName = "Jeff",
                    LastName = "Croft",
                    Email = "jc@mail.com",
                    PhoneNumber = "1234567891",
                    DateOfBirth = DateTime.Parse("1989-04-15"),
                },
                new Visitor
                {
                    FirstName = "Joe",
                    LastName = "Person",
                    Email = "jp@mail.com",
                    PhoneNumber = "1234567892",
                    DateOfBirth = DateTime.Parse("1990-04-15"),
                },
                new Visitor
                {
                    FirstName = "Jane",
                    LastName = "Mot",
                    Email = "jm@mail.com",
                    PhoneNumber = "1234567893",
                    DateOfBirth = DateTime.Parse("1991-04-15"),
                },
                new Visitor
                {
                    FirstName = "Jo",
                    LastName = "Zi",
                    Email = "jz@mail.com",
                    PhoneNumber = "1234567894",
                    DateOfBirth = DateTime.Parse("1992-04-15"),
                },
                new Visitor
                {
                    FirstName = "Jello",
                    LastName = "Yee",
                    Email = "jy@mail.com",
                    PhoneNumber = "1234567895",
                    DateOfBirth = DateTime.Parse("1993-04-15"),
                },
                new Visitor
                {
                    FirstName = "Jesus",
                    LastName = "Xo",
                    Email = "jx@mail.com",
                    PhoneNumber = "1234567896",
                    DateOfBirth = DateTime.Parse("1994-04-15"),
                }
           };
            foreach (Visitor vi in visitors)
            {
                    context.Visitors.Add(vi);
            }
            context.SaveChanges();

            var tickets = new Ticket[]
{
                new Ticket
                {
                    DatePurchased = DateTime.Parse("2009-10-03"),
                    TicketTypeId = ttypes.Single(s => s.Name == "Basic").Id,
                    VisitorId = visitors.Single(s => s.LastName == "Hop").Id,
                     DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id
                },
                new Ticket
                {
                    DatePurchased = DateTime.Parse("2009-10-03"),
                    TicketTypeId = ttypes.Single(s => s.Name == "Basic").Id,
                    VisitorId = visitors.Single(s => s.LastName == "Hope").Id,
                     DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id
                },
                new Ticket
                {
                    DatePurchased = DateTime.Parse("2009-10-03"),
                    TicketTypeId = ttypes.Single(s => s.Name == "Basic").Id,
                    VisitorId = visitors.Single(s => s.LastName == "Hopee").Id,
                     DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id
                },
                new Ticket
                {
                    DatePurchased = DateTime.Parse("2009-10-03"),
                    TicketTypeId = ttypes.Single(s => s.Name == "Basic").Id,
                    VisitorId = visitors.Single(s => s.LastName == "Hopeee").Id,
                     DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id
                },
                new Ticket
                {
                    DatePurchased = DateTime.Parse("2009-10-03"),
                    TicketTypeId = ttypes.Single(s => s.Name == "Basic").Id,
                    VisitorId = visitors.Single(s => s.LastName == "Croft").Id,
                     DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id
                },
                 new Ticket
                {
                    DatePurchased = DateTime.Parse("2009-10-03"),
                    TicketTypeId = ttypes.Single(s => s.Name == "Basic").Id,
                    VisitorId = visitors.Single(s => s.LastName == "Person").Id,
                     DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id
                },

                new Ticket
                {
                    DatePurchased = DateTime.Parse("2009-10-03"),
                    TicketTypeId = ttypes.Single(s => s.Name == "Basic").Id,
                    VisitorId = visitors.Single(s => s.LastName == "Mot").Id,
                     DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id
                },
                new Ticket
                {
                    DatePurchased = DateTime.Parse("2009-10-03"),
                    TicketTypeId = ttypes.Single(s => s.Name == "30-Day").Id,
                    VisitorId = visitors.Single(s => s.LastName == "Zi").Id,
                     DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id
                },
                new Ticket
                {
                    DatePurchased = DateTime.Parse("2009-10-03"),
                    TicketTypeId = ttypes.Single(s => s.Name == "30-Day").Id,
                    VisitorId = visitors.Single(s => s.LastName == "Yee").Id,
                     DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id
                },
                new Ticket
                {
                    DatePurchased = DateTime.Parse("2009-10-03"),
                    TicketTypeId = ttypes.Single(s => s.Name == "Premium").Id,
                    VisitorId = visitors.Single(s => s.LastName == "Xo").Id,
                     DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id
                }

};
            foreach (Ticket t in tickets) {
                var ticketSalesInDatabase = context.Tickets.Where(s => s.DailyReport.Id == t.DailyReportId).SingleOrDefault();
                if(ticketSalesInDatabase == null) context.Tickets.Add(t);
            }
            context.SaveChanges();




            var vtypes = new VendorType[]
            {
                new VendorType { Name = "Stand" },
                new VendorType { Name = "Store" },
                new VendorType { Name = "Game" },
            };
            foreach (VendorType vt in vtypes) { context.VendorTypes.Add(vt); }
            context.SaveChanges();


            var vendors = new Vendor[]
            {
                new Vendor
                {
                    ParkInfoId = pinfo.Single(s => s.Name == "Houston Theme Park").Id,
                    Name = "Tacos to Go",
                    Description = "Sells tacos to people on the go",
                    LocationId = locations.Single(s => s.Name == "West").Id,
                    VendorTypeId = vtypes.Single(s => s.Name == "Stand").Id,
                },
                new Vendor
                {
                    ParkInfoId = pinfo.Single(s => s.Name == "Houston Theme Park").Id,
                    Name = "Shoot Them All",
                    Description = "Shoot down all the targets win prizes",
                    LocationId = locations.Single(s => s.Name == "East").Id,
                    VendorTypeId = vtypes.Single(s => s.Name == "Game").Id,
                },
                new Vendor
                {
                    ParkInfoId = pinfo.Single(s => s.Name == "Houston Theme Park").Id,
                    Name = "Park Gifts",
                    Description = "Gifts for your friends who couldn't be here",
                    LocationId = locations.Single(s => s.Name == "Center").Id,
                    VendorTypeId = vtypes.Single(s => s.Name == "Store").Id,
                }
            };
            foreach (Vendor ve in vendors)
            {
                var vendorsInDatabase = context.Vendors.Where
                    (s => s.ParkInfo.Id == ve.ParkInfoId).SingleOrDefault();
                if(vendorsInDatabase == null) context.Vendors.Add(ve);
            }
            context.SaveChanges();

            var vsales = new VendorSale[]
            {
                new VendorSale
                {
                    ReportDate = DateTime.Parse("2009-10-03") ,
                    TotalSales = 73.85m,
                    SalesGoal = 100.00m,
                    VendorId = vendors.Single(s => s.Name == "Park Gifts").Id,
                     DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id
                },
                new VendorSale 
                { 
                    ReportDate = DateTime.Parse("2009-10-03") ,  
                    TotalSales = 150.00m,
                    SalesGoal = 75.00m,
                    VendorId = vendors.Single(s => s.Name == "Shoot Them All").Id,
                     DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id
                },
                new VendorSale 
                { 
                    ReportDate = DateTime.Parse("2009-10-03") ,  
                    TotalSales = 100.00m,
                    SalesGoal = 50.00m ,
                    VendorId = vendors.Single(s => s.Name == "Tacos to Go").Id,
                     DailyReportId = dreports.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id
                }
            };
            foreach (VendorSale ve in vsales)
            {
                var vendorSalesInDatabase = context.VendorSales.Where
                    (s => s.Vendor.Id == ve.VendorId &&
                     s.DailyReport.Id == ve.DailyReportId).SingleOrDefault();
                if (vendorSalesInDatabase == null)
                    context.VendorSales.Add(ve);
                
            }
            context.SaveChanges();


 

            /*
            var pinfo = new ParkInfo[]
            {
                new ParkInfo
                {
                    Name = "Houston Theme Park",
                    Country = "USA",
                    State = "TX",
                    City = "Houston",
                    ZipCode = "77042",
                    StreetNumber = "11223",
                    StreetName = "Grant Rd"
                },
            };
            foreach (ParkInfo pi in pinfo) { context.ParkInfo.Add(pi); }
            context.SaveChanges();
            */
            /*
            var dreports = new DailyReport[]
            {
                new DailyReport
                {
                    WeatherId = weather.Single(s => s.Date == DateTime.Parse("2009-10-03")).Id,
                    Date = DateTime.Parse("2009-10-03")
                }
            };

            foreach (DailyReport dr in dreports) { context.DailyReports.Add(dr); }

            context.SaveChanges();
            */

        }
    }
}

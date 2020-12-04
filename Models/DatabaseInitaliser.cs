using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace cw2_ssd.Models
{
    public class DatabaseInitaliser : DropCreateDatabaseIfModelChanges<TicketDbContext>
    {
        protected override void Seed(TicketDbContext context)
        {
            base.Seed(context);

            if (!context.Users.Any()) // Starts if statement if no users are present
            {
                // Creates an instance of role manager
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                //Creates roles for the role manager if they don't exist
                if (!roleManager.RoleExists("Admin"))
                {
                    roleManager.Create(new IdentityRole("Admin"));
                }
                if (!roleManager.RoleExists("Staff"))
                {
                    roleManager.Create(new IdentityRole("Staff"));
                }
                if (!roleManager.RoleExists("Customer"))
                {
                    roleManager.Create(new IdentityRole("Customer"));
                }
                
                // Save Changes for new roles
                context.SaveChanges();
                
                // Create Users
                UserManager<User> _userManager = new UserManager<User>(new UserStore<User>(context));
                
                // Create an Admin
                if (_userManager.FindByName("admin@ssts.com") == null)
                {
                    // Password Validation
                    _userManager.PasswordValidator = new PasswordValidator
                    {
                        RequireDigit = false,
                        RequiredLength = 6,
                        RequireLowercase = false,
                        RequireNonLetterOrDigit = false,
                        RequireUppercase = true,
                    };

                    var administrator = new Staff
                    {
                        UserName = "admin@ssts.com",
                        Email = "admin@ssts.com",
                        DateRegistered = DateTime.Now,
                        EmailConfirmed = true

                    };
                    _userManager.Create(administrator, "AdminPass");
                    _userManager.AddToRole(administrator.Id, "Admin");
                }
                
                // Create Staff
                if (_userManager.FindByName("sam@ssts.com") == null)
                {
                    var staffSam = new Staff
                    {
                        UserName = "sam@ssts.com",
                        Email = "sam@ssts.com",
                        DateRegistered = DateTime.Now,
                        EmailConfirmed = true

                    };
                    _userManager.Create(staffSam, "SammyEmer");
                    _userManager.AddToRole(staffSam.Id, "Staff");
                }
                
                if (_userManager.FindByName("florian@ssts.com") == null)
                {
                    var staffFlor = new Staff
                    {
                        UserName = "florian@ssts.com",
                        Email = "florian@ssts.com",
                        DateRegistered = DateTime.Now,
                        EmailConfirmed = true

                    };
                    _userManager.Create(staffFlor, "FlorianVanVain");
                    _userManager.AddToRole(staffFlor.Id, "Staff");
                }
                
                // Create Members
                if (_userManager.FindByName("Steven@google.com") == null)
                {
                    var onlyCustomer = new Customer
                    {
                        UserName = "Steven@google.com",
                        Email = "Steven@google.com",
                        DateRegistered = DateTime.Now,
                        CompanyName = "Google",
                        EmailConfirmed = true

                    };
                    _userManager.Create(onlyCustomer, "CustomerPass");
                    _userManager.AddToRole(onlyCustomer.Id, "Customer");
                }
                
                if (_userManager.FindByName("jimi@hendrix.com") == null)
                {
                    var onlyCustomer = new Customer
                    {
                        UserName = "jimi@hendrix.com",
                        Email = "jimi@hendrix.com",
                        DateRegistered = DateTime.Now,
                        CompanyName = "Hendrix",
                        EmailConfirmed = true

                    };
                    _userManager.Create(onlyCustomer, "PurpleHaze");
                    _userManager.AddToRole(onlyCustomer.Id, "Customer");
                }
                
                if (_userManager.FindByName("steve@vai.com") == null)
                {
                    var onlyCustomer = new Customer
                    {
                        UserName = "steve@vai.com",
                        Email = "steve@vai.com",
                        DateRegistered = DateTime.Now,
                        CompanyName = "Vai",
                        EmailConfirmed = true

                    };
                    _userManager.Create(onlyCustomer, "LoveOfGod");
                    _userManager.AddToRole(onlyCustomer.Id, "Customer");
                }

                // Save all users
                context.SaveChanges();
                
                // Create Example Tickets
                var closedTicket = new Ticket
                {
                    TicketID = Guid.NewGuid().ToString(),
                    StaffID = context.Users.Where(x => x.Email.Equals("admin@ssts.com")).FirstOrDefault().Id,
                    ErrorTitle = "Error found in Google files!",
                    ErrorDesc = "Logic error regarding search",
                    TicketDate = DateTime.Now,
                    TicketType = "Development",
                    TicketPriority = "Low",
                    TicketState = "Closed",
                    ClientCompany = "Vai"
                };
                context.Tickets.Add(closedTicket);
                context.SaveChanges();
                
                var openTicket = new Ticket
                {
                    TicketID = Guid.NewGuid().ToString(),
                    StaffID = context.Users.Where(x => x.Email.Equals("sam@ssts.com")).FirstOrDefault().Id,
                    ErrorTitle = "Can't push to Github!",
                    ErrorDesc = "Sorry guys, I'm the new guy and I don't know how Github works",
                    TicketDate = DateTime.Now,
                    TicketType = "Production",
                    TicketPriority = "High",
                    TicketState = "Open",
                    ClientCompany = "Hendrix",
                    ListOfComments = new List<Comment>()
                    {
                        new Comment()
                        {
                            CommentID = Guid.NewGuid().ToString(),
                            UserID = context.Users.Where(x => x.Email.Equals("admin@ssts.com")).FirstOrDefault().Id,
                            CommentTimestamp = DateTime.Now,
                            CommentText = "Oh boy, here we go"
                        },
                        new Comment()
                        {
                            CommentID = Guid.NewGuid().ToString(),
                            UserID = context.Users.Where(x => x.Email.Equals("sam@ssts.com")).FirstOrDefault().Id,
                            CommentTimestamp = DateTime.Now,
                            CommentText = "I take personal offence to that, don't you know I did a code bootcamp!"
                        },
                    }
                };
                context.Tickets.Add(openTicket);
                context.SaveChanges();
                
                var resolvedTicket = new Ticket
                {
                    TicketID = Guid.NewGuid().ToString(),
                    StaffID = context.Users.Where(x => x.Email.Equals("admin@ssts.com")).FirstOrDefault().Id,
                    ErrorTitle = "Consistent Crashes in the Hendrix Project",
                    ErrorDesc = "Everytime a whole number is inputted into the interface, a crash occurs",
                    TicketDate = DateTime.Now,
                    TicketType = "Testing",
                    TicketPriority = "Low",
                    TicketState = "Resolved",
                    ClientCompany = "Hendrix"
                };
                context.Tickets.Add(resolvedTicket);
                context.SaveChanges();
                
            } // End of if statement
        }
    }
}
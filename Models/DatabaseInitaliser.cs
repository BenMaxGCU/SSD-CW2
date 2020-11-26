using System;
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
                //Creates an instance of role manager
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
                
                // Save Changes
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
                    // Password Validation
                    _userManager.PasswordValidator = new PasswordValidator
                    {
                        RequireDigit = false,
                        RequiredLength = 6,
                        RequireLowercase = false,
                        RequireNonLetterOrDigit = false,
                        RequireUppercase = true,
                    };

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
                    // Password Validation
                    _userManager.PasswordValidator = new PasswordValidator
                    {
                        RequireDigit = false,
                        RequiredLength = 6,
                        RequireLowercase = false,
                        RequireNonLetterOrDigit = false,
                        RequireUppercase = true,
                    };

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
                
                // Create Staff
                if (_userManager.FindByName("onlyMember@google.com") == null)
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

                    var onlyCustomer = new Customer
                    {
                        UserName = "onlyMember@google.com",
                        Email = "onlyMember@google.com",
                        DateRegistered = DateTime.Now,
                        EmailConfirmed = true

                    };
                    _userManager.Create(onlyCustomer, "CustomerPass");
                    _userManager.AddToRole(onlyCustomer.Id, "Customer");
                }

                context.SaveChanges();
            } // End of if statement
        }
    }
}
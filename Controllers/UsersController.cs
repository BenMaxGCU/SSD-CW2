using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using cw2_ssd.Models;
using Microsoft.AspNet.Identity;

namespace cw2_ssd.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : AccountController
    {
        private TicketDbContext db = new TicketDbContext();

        public UsersController() : base()
        {

        }

        public UsersController(ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(userManager, signInManager)
        {

        }

        // GET: Users
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,DateRegistered,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,DateRegistered,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Admin"))]
        [ActionName("RegisterUser")]
        public async Task<ActionResult> RegisterUser([Bind(Include = "Email,Password,PasswordConfirm,UserRole")] RegisterUserViewModel model)
        {
            // Add new user to system
            if (ModelState.IsValid)
            {
                Staff regiUser = new Staff();
                regiUser.Email = model.Email;
                regiUser.UserName = model.Email;
                regiUser.DateRegistered = DateTime.Now;
                string chosenRole = model.UserRole;

                IdentityResult result = await UserManager.CreateAsync(regiUser, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(regiUser.Id, chosenRole);

                    return RedirectToAction("Index", "Users");
                } else
                {
                    return RedirectToAction("RegisterUser", "Users");
                }
            }
            return View(model);
        }
        
        [Authorize(Roles = "Admin")]
        public ActionResult RegisterCustomer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Admin"))]
        [ActionName("RegisterCustomer")]
        public async Task<ActionResult> RegisterCustomer([Bind(Include = "Email,Password,PasswordConfirm,ClientCompany")] RegisterCustomerViewModel model)
        {
            // Add new customer to system
            if (ModelState.IsValid)
            {
                Customer regiUser = new Customer();
                regiUser.Email = model.Email;
                regiUser.UserName = model.Email;
                regiUser.DateRegistered = DateTime.Now;
                regiUser.CompanyName = model.ClientCompany;

                IdentityResult result = await UserManager.CreateAsync(regiUser, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(regiUser.Id, "Customer");

                    return RedirectToAction("Index", "Users");
                } else
                {
                    return RedirectToAction("RegisterCustomer", "Users");
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddCompany(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer user = db.Users.Find(id) as Customer;

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(new AddCompanyViewModel
            {
                UserName = user.Email,
                NewCompany = user.CompanyName
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("AddCompany")]
        public async Task<ActionResult> AddCompany(string id,
            [Bind(Include = "NewCompany")] AddCompanyViewModel model)
        {
            if (ModelState.IsValid)
            {
                Customer user = (Customer)await UserManager.FindByIdAsync(id);
                UpdateModel(user);

                IdentityResult result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Users");
                }
            }

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

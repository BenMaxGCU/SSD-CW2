using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using cw2_ssd.Models;
using Microsoft.AspNet.Identity;

namespace cw2_ssd.Controllers
{
    public class TicketsController : Controller
    {
        private TicketDbContext db = new TicketDbContext();
        
        // GET: Tickets
        [Authorize(Roles = "Admin, Staff, Customer")]
        public ActionResult Index()
        {
            // Checks user for customer role
            if (User.IsInRole("Customer"))
            {
                // Finds current user and then gets their company name
                Customer user = (Customer) db.Users.Find(User.Identity.GetUserId());
                string clientCompany = user.CompanyName;
                
                // Filters tickets by the company name to ensure only customers from specific companies can view their tickets
                var tickets = db.Tickets.Where(b => b.ClientCompany.Equals(clientCompany));
                return View(tickets.ToList());
            }
            else
            {
                // Displays all tickets to Staff and Admin
                return View(db.Tickets.ToList());
            }
        }

        // GET: Tickets/Details/5
        [Authorize(Roles = "Admin, Staff, Customer")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            var comments = db.Comments.ToList();
            var user = db.Users.Where(x => x.Email.Equals(User.Identity.Name)).FirstOrDefault();
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Create([Bind(Include = "TicketID, StaffID,TicketDate,ErrorTitle,ErrorDesc,TicketType,TicketPriority,TicketState, ClientCompany")] TicketViewModel ticket)
        {
            if (ModelState.IsValid)
            {
                Ticket newTicket = new Ticket();
                string ticketId = Guid.NewGuid().ToString();
                
                newTicket.TicketID = ticketId;
                newTicket.StaffID = User.Identity.GetUserId();
                newTicket.ErrorTitle = ticket.ErrorTitle;
                newTicket.ErrorDesc = ticket.ErrorDesc;
                newTicket.TicketType = ticket.TicketType;
                newTicket.TicketPriority = ticket.TicketPriority;
                newTicket.TicketState = "Open";
                newTicket.TicketDate = DateTime.Now;
                newTicket.ClientCompany = ticket.ClientCompany;
                db.Tickets.Add(newTicket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticket);
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Edit([Bind(Include = "TicketID, StaffID,TicketDate,ErrorTitle,ErrorDesc,TicketType,TicketPriority,TicketState, ClientCompany")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult DeleteConfirmed(string id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
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

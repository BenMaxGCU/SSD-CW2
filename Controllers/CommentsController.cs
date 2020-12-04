using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using cw2_ssd.Models;
using Microsoft.AspNet.Identity;

namespace cw2_ssd.Controllers
{
    public class CommentsController : Controller
    {
        private TicketDbContext db = new TicketDbContext();

        // POST: Comments/Create
        [HttpPost]
        public JsonResult Create(string userId, string commentText, string ticketId)
        {
            if (userId != null)
            {
                var comment = new Comment()
                {
                    CommentText = commentText,
                    UserID = userId,
                    CommentTimestamp = DateTime.Now,
                    CommentID = Guid.NewGuid().ToString(),
                    TicketID = ticketId
                };
                db.Comments.Add(comment);
                return Json(comment.CommentID, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        

        // POST: Comments/Edit/5
        [HttpPost]
        public JsonResult Edit(string commentId, string commentText)
        {
            var comment = db.Comments.Find(commentId);
            
            if (comment.UserID.Equals(User.Identity.GetUserId()))
            {
                comment.CommentText = commentText;
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        // POST: Comments/Delete/5
        [HttpPost]
        public JsonResult Delete(string commentId)
        {
            var comment = db.Comments.Find(commentId);
            
            if (comment.UserID.Equals(User.Identity.GetUserId()))
            {
                db.Comments.Remove(comment);
                db.SaveChanges();
                
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cw2_ssd.Models
{
    public class Comment
    {
        [Key] 
        public string CommentID { get; set; }

        [ForeignKey("User")]
        public string UserID { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Ticket")]
        public string TicketID { get; set; }
        public virtual Ticket Ticket { get; set; }
        
        [Required]
        [Display(Name = "Comment Timestamp")]
        public DateTime? CommentTimestamp { get; set; }
        
        [Required]
        [Display(Name = "Comment Text")]
        public string CommentText { get; set; }
    }
}
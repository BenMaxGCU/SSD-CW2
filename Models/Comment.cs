using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cw2_ssd.Models
{
    public class Comment
    {
        [Key] 
        public int CommentID { get; set; }
        
        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }
        
        [ForeignKey("Ticket")]
        public int TicketID { get; set; }
        public Ticket Ticket { get; set; }
        
        [Required]
        [Display(Name = "Comment Timestamp")]
        public DateTime? CommentTimestamp { get; set; }
        
        [Required]
        [Display(Name = "Comment Text")]
        public string CommentText { get; set; }
    }
}
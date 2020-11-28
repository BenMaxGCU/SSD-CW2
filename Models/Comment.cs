using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cw2_ssd.Models
{
    public class Comment
    {
        [Key] 
        public string CommentID { get; set; }
        
        [ForeignKey("Customer")]
        public string CustomerID { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("Staff")]
        public string StaffID { get; set; }
        public Staff Staff { get; set; }

        [ForeignKey("Ticket")]
        public string TicketID { get; set; }
        public Ticket Ticket { get; set; }
        
        [Required]
        [Display(Name = "Comment Timestamp")]
        public DateTime? CommentTimestamp { get; set; }
        
        [Required]
        [Display(Name = "Comment Text")]
        public string CommentText { get; set; }
    }
}
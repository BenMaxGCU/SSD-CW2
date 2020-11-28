using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cw2_ssd.Models
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Ticket Timestamp")]
        public DateTime? TicketDate { get; set; }
        
        [Required]
        [Display(Name = "Error Title")]
        public string ErrorTitle { get; set; }
        
        [Required]
        [Display(Name = "Error Description")]
        public string ErrorDesc { get; set; }
        
        [Required]
        [Display(Name = "Ticket Type")]
        public string TicketType { get; set; }
        
        [Required]
        [Display(Name = "Ticket Priority")]
        public string TicketPriority { get; set; }
        
        [Required]
        [Display(Name = "Ticket State")]
        public string TicketState { get; set; }
        
        public List<Comment> ListOfComments { get; set; }

        public Ticket()
        {
            ListOfComments = new List<Comment>();
        }
    }
}
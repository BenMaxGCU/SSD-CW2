using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cw2_ssd.Models
{
    public class Ticket
    {
        [Key]
        public string TicketID { get; set; }
        
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
        
        [Required]
        [Display(Name = "Client Company")]
        public string ClientCompany { get; set; }
        
        [ForeignKey("Staff")]
        public string StaffID { get; set; }
        public Staff Staff { get; set; }
        
        public List<Comment> ListOfComments { get; set; }

        public Ticket()
        {
            ListOfComments = new List<Comment>();
        }

        public enum ticketType
        {
            Development,
            Testing,
            Production
        }

        public enum ticketPriority
        {
            High,
            Mid,
            Low
        }
        
        public enum ticketState
        {
            Open,
            Closed,
            Resolved
        }

        public enum companyName
        {
            Hendrix,
            Vai,
            Google
        }
    }
}
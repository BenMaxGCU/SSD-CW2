using System;
using System.ComponentModel.DataAnnotations;

namespace cw2_ssd.Models
{
    public class TicketViewModel
    {
        public string TicketID { get; set; }
        
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
        
        [Display(Name = "Ticket State")]
        public string TicketState { get; set; }
        
        [Required]
        [Display(Name = "Client Company")]
        public string ClientCompany { get; set; }
        
        public string StaffID { get; set; }
        public Staff Staff { get; set; }
    }
}
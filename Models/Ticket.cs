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
        [Display(Name = "Ticket Creation Date")]
        public DateTime? ticketDate { get; set; }
    }
}
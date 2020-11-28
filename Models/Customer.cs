﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace cw2_ssd.Models
{
    public class Customer : User
    {
        // Originally intended to use a customer id variable but I now handle the id with UserID
        
        /// <summary>
        /// Variable to distinguish clients from each other by including their company's name
        /// This allows me to filter what they see
        /// </summary>
        [Required]
        [Display(Name = "Client Company")] 
        public string CompanyName { get; set; }
        
    }
}
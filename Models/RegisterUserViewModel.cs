using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cw2_ssd.Models
{
    public class RegisterUserViewModel
    {
        /// <summary>
        /// User's email address
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("PasswordConfirm")]
        public string Password { get; set; }

        /// <summary>
        /// When edited, the user's password must be re-entered
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "User Role")]
        public string UserRole { get; set; }

        public enum rolesSelect
        {
            Customer,
            Staff,
            Admin
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace cw2_ssd.Models
{
    public class RegisterCustomerViewModel
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

        [Display(Name = "Client Company")]
        public string ClientCompany { get; set; }

        public enum companiesList
        {
            Hendrix,
            Vai,
            Google
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace cw2_ssd.Models
{
    public class AddCompanyViewModel
    {
        /// <summary>
        /// User's unique id in this view model
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// New company for the user
        /// </summary>
        [Display(Name = "Client Company")]
        public string NewCompany { get; set; }

        public enum companies
        {
            Hendrix,
            Vai,
            Google
        }
    }
}
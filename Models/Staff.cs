using System.ComponentModel.DataAnnotations;

namespace cw2_ssd.Models
{
    public class Staff : User
    {
        [Display(Name = "Job Title")]
        public string jobTitle { get; set; }
    }
}
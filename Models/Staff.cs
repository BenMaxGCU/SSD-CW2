using System.ComponentModel.DataAnnotations;

namespace cw2_ssd.Models
{
    public class Staff : User
    {
        [Key]
        public string StaffID { get; set; }

        [Display(Name = "Job Title")]
        public string jobTitle { get; set; }
    }
}
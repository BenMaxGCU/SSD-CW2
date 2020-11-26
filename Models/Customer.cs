using System.ComponentModel.DataAnnotations;

namespace cw2_ssd.Models
{
    public class Customer : User
    {
        [Key]
        public string CustomerID { get; set; }
        
    }
}
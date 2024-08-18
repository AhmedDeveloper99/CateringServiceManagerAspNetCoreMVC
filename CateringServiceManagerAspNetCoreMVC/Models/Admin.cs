using System.ComponentModel.DataAnnotations;

namespace CateringServiceManagerAspNetCoreMVC.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;

namespace CateringServiceManagerAspNetCoreMVC.Models
{
    public class Daig
    {
        [Key]
        public int DaigId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Status { get; set; } // Possible values: "In Use", "Idle", "Unavailable"
    }
}

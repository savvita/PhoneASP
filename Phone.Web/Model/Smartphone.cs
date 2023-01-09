using System.ComponentModel.DataAnnotations;

namespace Phone.Web.Model
{
    public class Smartphone
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Producer { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Model { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }
    }
}

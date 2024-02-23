using System.ComponentModel.DataAnnotations;

namespace ParksApi.Models
{
    public class Park
    {
        public int ParkId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string User { get; set; }
        public string ImageUrl { get; set; }
        public bool Camping { get; set; }
        public bool DiscGolf { get; set; }
        public bool Kayaking { get; set; }
        public bool BeachAccess { get; set; }
    }
}
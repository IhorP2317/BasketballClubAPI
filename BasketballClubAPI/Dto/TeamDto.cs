using BasketballClubAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace BasketballClubAPI.Dto {
    public class TeamDto {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters!")]
        public string Name { get; set; } 
        public int? HeadCoachId { get; set; }
    }
}

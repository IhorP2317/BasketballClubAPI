using BasketballClubAPI.Helper;
using BasketballClubAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace BasketballClubAPI.Dto {
    public class MatchDto {
        public int Id { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [MinLength(2, ErrorMessage = "Location must be at least 2 characters!")]
        public string Location { get; set; }

        [Required(ErrorMessage = "HomeTeamId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "HomeTeamId must be a positive number!")]
        public int HomeTeamId { get; set; }

        [Required(ErrorMessage = "AwayTeamId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "AwayTeamId must be a positive number!")]
        public int AwayTeamId { get; set; }

        [Required(ErrorMessage = "Status of match is required!")]
        [EnumValue(typeof(MatchStatus), ErrorMessage = "Invalid Status of match!")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        public DateTime? StartTime { get; set; }
    }
}

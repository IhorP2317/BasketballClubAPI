using System.ComponentModel.DataAnnotations;

namespace BasketballClubAPI.Dto {
    public class StatisticDto {
        [Required(ErrorMessage = "MatchId is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "MatchId must be a positive number!")]
        public int MatchId { get; set; }
        [Required(ErrorMessage = "PlayerId is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "PlayerId must be a positive number!")]
        public int PlayerId { get; set; }
        [Required(ErrorMessage = "Points is required!")]
        [Range(0, int.MaxValue, ErrorMessage = "Points must be a not negative number!")]
        public int Points { get; set; }
        [Required(ErrorMessage = "Assists is required!")]
        [Range(0, int.MaxValue, ErrorMessage = "Assists must be a not negative number!")]
        public int Assists { get; set; }
        [Required(ErrorMessage = "Rebounds is required!")]
        [Range(0, int.MaxValue, ErrorMessage = "Rebounds must be a not negative number!")]
        public int Rebounds { get; set; }
        [Required(ErrorMessage = "Steals is required!")]
        [Range(0, int.MaxValue, ErrorMessage = "Steals must be a not negative number!")]
        public int Steals { get; set; }
        [Required(ErrorMessage = "Blocks is required!")]
        [Range(0, int.MaxValue, ErrorMessage = "Blocks must be a not negative number!")]
        public int Blocks { get; set; }
        [Required(ErrorMessage = "Turnovers is required!")]
        [Range(0, int.MaxValue, ErrorMessage = "Turnovers must be a not negative number!")]
        public int Turnovers { get; set; }
    }
}

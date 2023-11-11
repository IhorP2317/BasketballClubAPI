using System.Text.Json.Serialization;

namespace BasketballClubAPI.Models {
    public class Statistic {
        public int MatchId { get; set; }
        public int PlayerId { get; set; }
        public int Points { get; set; }
        public int Assists { get; set; }
        public int Rebounds { get; set; }
        public int Steals { get; set; }
        public int Blocks { get; set; }
        public int Turnovers { get; set; }

        // Navigation property for the Match
        [JsonIgnore]
        public Match Match { get; set; } = null!;

        // Navigation property for the Player
        [JsonIgnore]
        public Player Player { get; set; } = null!;

    }
}

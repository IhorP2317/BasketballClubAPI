using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BasketballClubAPI.Models {
    public class Match {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Location { get; set; } = null!;
        public int HomeTeamId { get; set; } // Foreign key for HomeTeam
        public int AwayTeamId { get; set; } // Foreign key for AwayTeam
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public DateTime StartTime { get; set; }
        [EnumDataType(typeof(MatchStatus))]
        public MatchStatus Status { get; set; }
        // Navigation property for the Home Team
        [JsonIgnore]
        public Team HomeTeam { get; set; } = null!;

        // Navigation property for the Away Team
        [JsonIgnore]
        public Team AwayTeam { get; set; } = null!;
        // Navigation property for the Statistics related to this match
        [JsonIgnore]
        public ICollection<Statistic> Statistics { get; set; } = null!;
    }
}

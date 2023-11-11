using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BasketballClubAPI.Models {
    public class Team {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Make it nullable
        public string Name { get; set; } = null!;
        public int? HeadCoachId { get; set; }
        [JsonIgnore]
        public ICollection<Player> Players { get; set; } = null!;
        [JsonIgnore]
        public Coach HeadCoach { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BasketballClubAPI.Models {
    public class Player : Staff {
        public double Height { get; set; }
        public double Weight { get; set; }
        [EnumDataType(typeof(Position))]
        public Position Position { get; set; }
        [JsonIgnore]
        public ICollection<Statistic> Statistics { get; set; } = null!;
        public int? TeamId { get; set; }
        [JsonIgnore]
        public Team Team { get; set; } 
    }
}

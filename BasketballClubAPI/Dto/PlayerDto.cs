using BasketballClubAPI.Helper;
using BasketballClubAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace BasketballClubAPI.Dto {
    public class PlayerDto {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is required!")]
        [MinLength(2, ErrorMessage = "First Name must be at least 2 characters!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [MinLength(2, ErrorMessage = "Last Name must be at least 2 characters!")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Date Of Birth is required!")]
        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [MinimumAge(18, ErrorMessage = "Age must be at least 18 years!")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Country is required!")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Height is required!")]
        [Range(100, int.MaxValue , ErrorMessage = "Height must be at least 100 cm!")]
        public double Height { get; set; }
        [Required(ErrorMessage = "Weight is required!")]
        [Range(40, 200, ErrorMessage = "Weight must be between 40 and 200 kg.")]
        public double Weight { get; set; }
        [Required(ErrorMessage = "Position is required!")]
        [EnumValue(typeof(Position), ErrorMessage = "Invalid Position. Position must be 'guard', 'forward' or 'center'!")]
        public string Position { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "TeamId must be a positive number!")]
        public int? TeamId { get; set; }
    }
}

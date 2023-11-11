using BasketballClubAPI.Helper;
using System.ComponentModel.DataAnnotations;

namespace BasketballClubAPI.Dto {
    public class CoachDto {
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
        [Required(ErrorMessage = "Coaching specialty is required!")]
        [MinLength(7, ErrorMessage = "Coaching specialty must be at least 7 characters!")]
        public string CoachingSpecialty { get; set; } 
    }
}

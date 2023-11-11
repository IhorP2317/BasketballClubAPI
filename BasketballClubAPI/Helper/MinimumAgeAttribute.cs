using System.ComponentModel.DataAnnotations;

namespace BasketballClubAPI.Helper {
    public class MinimumAgeAttribute : ValidationAttribute {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge) {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if (value is DateTime dateOfBirth) {
                // Calculate the age by comparing with the current date
                int age = DateTime.Today.Year - dateOfBirth.Year;

                // Adjust the age if the birth date hasn't occurred yet this year
                if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) {
                    age--;
                }

                if (age < _minimumAge) {
                    return new ValidationResult($"Age must be at least {_minimumAge} years.");
                }
            }

            return ValidationResult.Success;
        }
    }
}

namespace BasketballClubAPI.Helper {
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class EnumValueAttribute : ValidationAttribute {
        private readonly Type _enumType;

        public EnumValueAttribute(Type enumType) {
            if (enumType is null || !enumType.IsEnum) {
                throw new ArgumentException("The provided type is not an enum.", nameof(enumType));
            }

            _enumType = enumType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if (Enum.IsDefined(_enumType, value)) {
                return ValidationResult.Success;
            }

            return new ValidationResult($"{value} is not a valid {_enumType.Name} value.");
        }
    }
}

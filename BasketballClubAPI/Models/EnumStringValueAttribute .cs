namespace BasketballClubAPI.Models {
    public class EnumStringValueAttribute : Attribute {
        public string Value { get; }

        public EnumStringValueAttribute(string value) {
            Value = value;
        }
    }
}

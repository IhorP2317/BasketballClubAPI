using BasketballClubAPI.Models;

namespace BasketballClubAPI.Helper {
    public static class EnumExtension {
        public static string GetStringValue(this Enum value) {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = fieldInfo.GetCustomAttributes(typeof(EnumStringValueAttribute), false) as EnumStringValueAttribute[];
            return attributes?.Length > 0 ? attributes[0].Value : null;
        }
        public static TEnum ParseEnum<TEnum>(string value) where TEnum : struct {
            if (Enum.TryParse<TEnum>(value, out TEnum result)) {
                return result;
            }

            throw new ArgumentException($"Value '{value}' is not a valid enum for {typeof(TEnum).Name}");
        }
    }
}

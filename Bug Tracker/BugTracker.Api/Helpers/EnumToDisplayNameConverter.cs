using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BugTracker.Api.Helpers
{
    public class EnumToDisplayNameConverter <T> : JsonConverter<T> where T : struct, Enum
    {
        public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var enumString = reader.Value.ToString();
                var enumType = typeof(T);
                var enumValues = Enum.GetValues(enumType).Cast<T>();

                foreach (var enumValue in enumValues)
                {
                    var displayAttribute = enumType.GetField(enumValue.ToString())
                                                   .GetCustomAttribute<DisplayAttribute>();

                    if (displayAttribute != null && displayAttribute.Name == enumString)
                    {
                        return enumValue;
                    }
                }

                throw new JsonSerializationException($"Unable to convert \"{enumString}\" to enum \"{enumType}\".");
            }

            throw new JsonSerializationException("Expected string token.");
        }

        public override void WriteJson(JsonWriter writer, T value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var enumType = typeof(T);
            var displayAttribute = enumType.GetField(value.ToString())
                                           ?.GetCustomAttribute<DisplayAttribute>();

            if (displayAttribute != null)
            {
                writer.WriteValue(displayAttribute.Name);
            }
            else
            {
                writer.WriteValue(value.ToString());
            }
        }
    }
}

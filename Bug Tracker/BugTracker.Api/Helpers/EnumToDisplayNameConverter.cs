using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BugTracker.Api.Helpers
{
    public class EnumToDisplayNameConverter <T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var enumType = typeof(T);
            var enumValues = Enum.GetValues(enumType).Cast<T>();

            foreach (var enumValue in enumValues)
            {
                var displayAttribute = enumType.GetField(enumValue.ToString())
                                               .GetCustomAttribute<DisplayAttribute>();

                if (displayAttribute != null && displayAttribute.Name == reader.GetString())
                {
                    return enumValue;
                }
            }

            throw new JsonException($"Unable to convert \"{reader.GetString()}\" to enum \"{enumType}\".");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var enumType = typeof(T);
            var displayAttribute = enumType.GetField(value.ToString())
                                           .GetCustomAttribute<DisplayAttribute>();

            if (displayAttribute != null)
            {
                writer.WriteStringValue(displayAttribute.Name);
            }
            else
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}

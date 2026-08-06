using RocketLeagueGameDataAPI.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.JsonConverters
{
	/// <summary>
	/// Converts a <see cref="DateTime"/> to or from it's <see langword="string"/> representation.
	/// </summary>
	public class JsonDateTimeConverter : JsonConverter<DateTime>
	{
		public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return DateTime.ParseExact(reader.GetString()!, "yyyy-MM-dd HH-mm-ss", null);
		}

		public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString("yyyy-MM-dd HH-mm-ss"));
		}
	}
}

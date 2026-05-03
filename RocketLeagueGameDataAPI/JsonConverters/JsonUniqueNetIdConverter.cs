using RocketLeagueGameDataAPI.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.JsonConverters
{
	/// <summary>
	/// Converts a <see cref="UniqueNetId"/> to or from it's <see langword="string"/> representation.
	/// </summary>
	public class JsonUniqueNetIdConverter : JsonConverter<UniqueNetId>
	{
		public override UniqueNetId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return UniqueNetId.Parse(reader.GetString()!);
		}

		public override void Write(Utf8JsonWriter writer, UniqueNetId value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString());
		}
	}
}

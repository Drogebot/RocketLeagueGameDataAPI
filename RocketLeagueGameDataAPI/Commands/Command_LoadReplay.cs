using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Commands
{
	/// <summary>
	/// Loads and begins playback of a replay by file name or by file path.
	/// </summary>
	[JsonSerializable(typeof(Command_LoadReplay))]
	public class Command_LoadReplay : CommandData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override CommandType CommandType => CommandType.LoadReplay;
		/// <summary>
		/// Name of the replay file to play. Takes precedence over Path when both are set.
		/// </summary>
		/// <remarks>ONE OF.</remarks>
		public string? FileName { get; set; }
		/// <summary>
		/// File path of a replay to play. Used when FileName is empty.
		/// </summary>
		/// <remarks>ONE OF.</remarks>
		public string? Path { get; set; }
	}
}

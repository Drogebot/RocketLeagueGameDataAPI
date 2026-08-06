using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Commands
{
	/// <summary>
	/// Pauses or unpauses the current match or replay.
	/// </summary>
	[JsonSerializable(typeof(Command_SetMatchPaused))]
	public class Command_SetMatchPaused : CommandData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override CommandType CommandType => CommandType.SetMatchPaused;
		/// <summary>
		/// <see langword="true"/> pauses; <see langword="false"/> resumes.
		/// </summary>
		public required bool bPaused { get; set; }
	}
}

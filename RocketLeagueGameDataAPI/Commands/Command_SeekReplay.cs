using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Commands
{
	/// <summary>
	/// Jumps replay playback to a target frame or time.
	/// </summary>
	/// <remarks>REPLAY.</remarks>
	[JsonSerializable(typeof(Command_SeekReplay))]
	public class Command_SeekReplay : CommandData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override CommandType CommandType => CommandType.SeekReplay;
		/// <summary>
		/// Target frame number. Takes precedence over TimeSeconds when set.
		/// </summary>
		/// <remarks>ONE OF.</remarks>
		public int? Frame { get; set; }
		/// <summary>
		/// Target time in seconds.
		/// </summary>
		/// <remarks>ONE OF.</remarks>
		public float? TimeSeconds { get; set; }
	}
}

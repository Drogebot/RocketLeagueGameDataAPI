using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Commands
{
	/// <summary>
	/// Sets replay playback speed.
	/// </summary>
	/// <remarks>REPLAY.</remarks>
	[JsonSerializable(typeof(Command_SetGameSpeed))]
	public class Command_SetGameSpeed : CommandData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override CommandType CommandType => CommandType.SetGameSpeed;
		/// <summary>
		/// Playback multiplier. 1.0 = normal, 0.5 = half speed, 2.0 = double. Must be ≥ 0.
		/// </summary>
		public required float Speed { get; set; }
	}
}

using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when the in-game clock has changed.
	/// </summary>
	[JsonSerializable(typeof(Event_ClockUpdatedSeconds))]
	public class Event_ClockUpdatedSeconds : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.ClockUpdatedSeconds;
		/// <summary>
		/// econds remaining in the match.
		/// </summary>
		public required int TimeSeconds { get; set; }
		/// <summary>
		/// <see langword="True"/> if the game is in overtime.
		/// </summary>
		public required bool bOvertime { get; set; }
	}
}

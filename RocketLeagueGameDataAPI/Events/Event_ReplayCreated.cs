using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when a replay is initialized. Does not pertain to goal replays, only replays you load via the Match History menu.
	/// </summary>
	[JsonSerializable(typeof(Event_ReplayCreated))]
	public class Event_ReplayCreated : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.ReplayCreated;
		/// <summary>
		/// File name of the replay.
		/// </summary>
		public required string FileName { get; set; }
		/// <summary>
		/// Timestamp the replay was created.
		/// </summary>
		public required DateTime Date { get; set; }
	}
}

using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Base event class for all events.
	/// </summary>
	[JsonSerializable(typeof(EventData))]
	public abstract class EventData
	{
		/// <summary>
		/// The <see cref="EventType"/> of this event.
		/// </summary>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public abstract EventType EventType { get; }
		/// <summary>
		/// Only set for online or LAN matches.
		/// </summary>
		public required string MatchGuid { get; set; }
	}
}

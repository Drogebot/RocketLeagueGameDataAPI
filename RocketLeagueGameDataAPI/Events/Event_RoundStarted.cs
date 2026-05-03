using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when the game enters the active state (after the countdown finishes).
	/// </summary>
	[JsonSerializable(typeof(Event_RoundStarted))]
	public class Event_RoundStarted : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.RoundStarted;
	}
}

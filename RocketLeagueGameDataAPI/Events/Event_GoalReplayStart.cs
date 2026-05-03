using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when a goal replay starts.
	/// </summary>
	[JsonSerializable(typeof(Event_GoalReplayStart))]
	public class Event_GoalReplayStart : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.ReplayPlaybackStart;
	}
}

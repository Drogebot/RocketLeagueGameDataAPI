using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when a goal replay ends.
	/// </summary>
	[JsonSerializable(typeof(Event_GoalReplayEnd))]
	public class Event_GoalReplayEnd : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.ReplayPlaybackEnd;
	}
}

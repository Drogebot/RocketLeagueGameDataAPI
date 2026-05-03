using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when the ball explodes during a goal replay. If the replay is skipped this event will not fire.
	/// </summary>
	[JsonSerializable(typeof(Event_GoalReplayWillEnd))]
	public class Event_GoalReplayWillEnd : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.ReplayWillEnd;
	}
}

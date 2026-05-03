using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent at the start of each round when the countdown starts.
	/// </summary>
	[JsonSerializable(typeof(Event_CountdownBegin))]
	public class Event_CountdownBegin : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.CountdownBegin;
	}
}

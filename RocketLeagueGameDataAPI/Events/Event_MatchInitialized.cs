using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when the first countdown starts.
	/// </summary>
	[JsonSerializable(typeof(Event_MatchInitialized))]
	public class Event_MatchInitialized : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.MatchInitialized;
	}
}

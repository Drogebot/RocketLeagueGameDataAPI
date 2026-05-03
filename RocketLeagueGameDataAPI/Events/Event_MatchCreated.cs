using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when all teams are created and replicated.
	/// </summary>
	[JsonSerializable(typeof(Event_MatchCreated))]
	public class Event_MatchCreated : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.MatchCreated;
	}
}

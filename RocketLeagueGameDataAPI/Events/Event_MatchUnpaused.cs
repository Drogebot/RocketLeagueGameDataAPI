using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when the game is unpaused by a match admin.
	/// </summary>
	[JsonSerializable(typeof(Event_MatchUnpaused))]
	public class Event_MatchUnpaused : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.MatchUnpaused;
	}
}

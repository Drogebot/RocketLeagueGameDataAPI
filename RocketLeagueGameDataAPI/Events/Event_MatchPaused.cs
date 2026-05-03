using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when the game is paused by a match admin.
	/// </summary>
	[JsonSerializable(typeof(Event_MatchPaused))]
	public class Event_MatchPaused : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.MatchPaused;
	}
}

using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when leaving the game.
	/// </summary>
	[JsonSerializable(typeof(Event_MatchDestroyed))]
	public class Event_MatchDestroyed : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.MatchDestroyed;
	}
}

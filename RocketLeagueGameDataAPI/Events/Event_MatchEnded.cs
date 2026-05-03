using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when the match ends and a winner is chosen.
	/// </summary>
	[JsonSerializable(typeof(Event_MatchEnded))]
	public class Event_MatchEnded : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.MatchEnded;
		/// <summary>
		/// Team index of the winning team.
		/// </summary>
		public required int WinnerTeamNum { get; set; }
	}
}

using RocketLeagueGameDataAPI.Models;
using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent X amount of times per second based on the player's PacketSendRate preference.
	/// </summary>
	[JsonSerializable(typeof(Event_UpdateState))]
	public class Event_UpdateState : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.UpdateState;
		/// <summary>
		/// One entry per player in the match.
		/// </summary>
		public required PlayerData[] Players { get; set; }
		/// <summary>
		/// Match metadata.
		/// </summary>
		public required MatchData Game { get; set; }
	}
}

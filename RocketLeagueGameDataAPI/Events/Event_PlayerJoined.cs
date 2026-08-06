using RocketLeagueGameDataAPI.Models;
using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when a player is added to the current match.
	/// </summary>
	[JsonSerializable(typeof(Event_PlayerJoined))]
	public class Event_PlayerJoined : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.PlayerJoined;
		/// <summary>
		/// Display name of the player who joined.
		/// </summary>
		public required string PlayerName { get; set; }
		/// <summary>
		/// Platform identifier in the format Platform|Uid|Splitscreen (e.g. "Steam|123|0", "Epic|456|0").
		/// </summary>
		public required UniqueNetId PrimaryId { get; set; }
	}
}

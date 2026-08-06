using RocketLeagueGameDataAPI.Models;
using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when a player is removed from the current match.
	/// </summary>
	[JsonSerializable(typeof(Event_PlayerLeft))]
	public class Event_PlayerLeft : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.PlayerLeft;
		/// <summary>
		/// Display name of the player who left.
		/// </summary>
		public required string PlayerName { get; set; }
		/// <summary>
		/// Platform identifier in the format Platform|Uid|Splitscreen (e.g. "Steam|123|0", "Epic|456|0").
		/// </summary>
		public required UniqueNetId PrimaryId { get; set; }
	}
}

using RocketLeagueGameDataAPI.Models;
using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when a vehicle collects a boost pad or pill.
	/// </summary>
	/// <remarks>SPECTATOR.</remarks>
	[JsonSerializable(typeof(Event_BoostPickup))]
	public class Event_BoostPickup : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.BoostPickup;
		/// <summary>
		/// The player who collected the boost.
		/// </summary>
		public required PlayerShortcut Player { get; set; }
		/// <summary>
		/// World location of the pickup, as { X, Y, Z }.
		/// </summary>
		public required Vector Location { get; set; }
		/// <summary>
		/// Amount of boost granted by the pickup.
		/// </summary>
		public required float BoostAmount { get; set; }
		/// <summary>
		/// Pickup class: BoostType_Pad (small pad), BoostType_Pill (full/big boost).
		/// </summary>
		public required BoostType BoostType { get; set; }
		/// <summary>
		/// True if the pickup occurred during replay playback.
		/// </summary>
		public required bool bReplay { get; set; }
	}
	public enum BoostType
	{
		/// <summary>Small pad.</summary>
		BoostType_Pad,
		/// <summary>Full/Big boost.</summary>
		BoostType_Pill,
	}
}

using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Models
{
	[JsonSerializable(typeof(PlayerShortcut))]
	public class PlayerShortcut
	{
		/// <summary>
		/// /// Display name.
		/// </summary>
		public required string Name { get; set; }
		/// <summary>
		/// Spectator shortcut number.
		/// </summary>
		public required int Shortcut { get; set; }
		/// <summary>
		/// Team index (0 = Blue, 1 = Orange).
		/// </summary>
		public required int TeamNum { get; set; }
	}
}

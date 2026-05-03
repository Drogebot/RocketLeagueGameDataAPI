using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Models
{
	[JsonSerializable(typeof(TeamData))]
	public class TeamData
	{
		/// <summary>
		/// Team name.
		/// </summary>
		public required string Name { get; set; }
		/// <summary>
		/// Team index.
		/// </summary>
		public required int TeamNum { get; set; }
		/// <summary>
		/// Team goal count.
		/// </summary>
		public required int Score { get; set; }
		/// <summary>
		/// Hex color code (no #) for the team’s primary color.
		/// </summary>
		public required string ColorPrimary { get; set; }
		/// <summary>
		/// Hex color code for the team’s secondary color.
		/// </summary>
		public required string ColorSecondary { get; set; }
	}
}

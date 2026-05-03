using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Models
{
	[JsonSerializable(typeof(BallTouchData))]
	public class BallTouchData
	{
		/// <summary>
		/// The player who made the last touch.
		/// </summary>
		public required PlayerShortcut Player { get; set; }
		/// <summary>
		/// Speed of the ball resulting from this hit.
		/// </summary>
		public required float Speed { get; set; }
	}
}

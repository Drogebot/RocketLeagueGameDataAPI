using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Models
{
	[JsonSerializable(typeof(BallHitData))]
	public class BallHitData
	{
		/// <summary>
		/// Ball speed before the hit (Unreal Units/second).
		/// </summary>
		public required float PreHitSpeed { get; set; }
		/// <summary>
		/// Ball speed after the hit (Unreal Units/second).
		/// </summary>
		public required float PostHitSpeed { get; set; }
		/// <summary>
		/// World position (X, Y, Z) of the ball at impact.
		/// </summary>
		public required Vector Location { get; set; }
	}
}

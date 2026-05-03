using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Models
{
	[JsonSerializable(typeof(BallData))]
	public class BallData
	{
		/// <summary>
		/// Current ball speed in Unreal Units/second.
		/// </summary>
		public required float Speed { get; set; }
		/// <summary>
		/// Index of the last team to touch the ball. 255 if the ball has not been touched.
		/// </summary>
		public required int TeamNum { get; set; } = 255;
	}
}

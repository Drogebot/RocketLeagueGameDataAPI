using RocketLeagueGameDataAPI.Models;
using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent one frame after the ball is hit.
	/// </summary>
	[JsonSerializable(typeof(Event_BallHit))]
	public class Event_BallHit : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.BallHit;
		/// <summary>
		/// Players that hit the ball that frame.
		/// </summary>
		public required PlayerShortcut[] Players { get; set; }
		/// <summary>
		/// Ball state at the moment of the hit.
		/// </summary>
		public required BallHitData Ball { get; set; }
	}
}

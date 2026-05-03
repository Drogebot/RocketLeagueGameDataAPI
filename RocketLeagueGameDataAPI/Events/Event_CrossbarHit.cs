using RocketLeagueGameDataAPI.Models;
using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when the ball hits a crossbar.
	/// </summary>
	[JsonSerializable(typeof(Event_CrossbarHit))]
	public class Event_CrossbarHit : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.CrossbarHit;
		/// <summary>
		/// World position (X, Y, Z) of the ball when the impact occurred.
		/// </summary>
		public required Vector BallLocation { get; set; }
		/// <summary>
		/// Ball speed on impact.
		/// </summary>
		public required float BallSpeed { get; set; }
		/// <summary>
		/// Impact force of the ball relative to the crossbar normal.
		/// </summary>
		public required float ImpactForce { get; set; }
		/// <summary>
		/// The last touch of the ball before the crossbar hit.
		/// </summary>
		public required BallTouchData BallLastTouch { get; set; }
	}
}

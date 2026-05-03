using RocketLeagueGameDataAPI.Models;
using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Events
{
	/// <summary>
	/// Sent when a goal is scored.
	/// </summary>
	[JsonSerializable(typeof(Event_GoalScored))]
	public class Event_GoalScored : EventData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override EventType EventType => EventType.GoalScored;
		/// <summary>
		/// Speed of the ball (Unreal Units/second) when it crossed the goal line.
		/// </summary>
		public required float GoalSpeed { get; set; }
		/// <summary>
		/// Length of the previous round in seconds.
		/// </summary>
		public required float GoalTime { get; set; }
		/// <summary>
		/// World position (X, Y, Z) of the ball when the goal was scored.
		/// </summary>
		public required Vector ImpactLocation { get; set; }
		/// <summary>
		/// The player who scored the goal.
		/// </summary>
		public required PlayerShortcut Scorer { get; set; }
		/// <summary>
		/// Same shape as Scorer. Present only when an assist was recorded.
		/// </summary>
		/// <remarks>CONDITIONAL</remarks>
		public PlayerShortcut? Assister { get; set; }
		/// <summary>
		/// The last touch of the ball before the goal.
		/// </summary>
		public required BallTouchData BallLastTouch { get; set; }
	}
}

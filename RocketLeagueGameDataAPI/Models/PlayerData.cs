using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Models
{
	[JsonSerializable(typeof(PlayerData))]
	public class PlayerData : PlayerShortcut
	{
		/// <summary>
		/// Platform identifier in the format Platform|Uid|Splitscreen (e.g. "Steam|123|0", "Epic|456|0").
		/// </summary>
		public required UniqueNetId PrimaryId { get; set; }
		/// <summary>
		/// Total match score.
		/// </summary>
		public required int Score { get; set; }
		/// <summary>
		/// Goals scored this match.
		/// </summary>
		public required int Goals { get; set; }
		/// <summary>
		/// Shot attempts this match.
		/// </summary>
		public required int Shots { get; set; }
		/// <summary>
		/// Assists earned this match.
		/// </summary>
		public required int Assists { get; set; }
		/// <summary>
		/// Saves made this match.
		/// </summary>
		public required int Saves { get; set; }
		/// <summary>
		/// Total ball touches.
		/// </summary>
		public required int Touches { get; set; }
		/// <summary>
		/// Touches by the car body (not ball).
		/// </summary>
		public required int CarTouches { get; set; }
		/// <summary>
		/// Demolitions inflicted.
		/// </summary>
		public required int Demos { get; set; }
		/// <summary>
		/// <see langword="True"/> if the player currently has a vehicle.
		/// </summary>
		/// <remarks>SPECTATOR.</remarks>
		public bool? bHasCar { get; set; }
		/// <summary>
		/// Vehicle speed in Unreal Units/second.
		/// </summary>
		/// <remarks>SPECTATOR.</remarks>
		public float? Speed { get; set; }
		/// <summary>
		/// Boost amount 0–100.
		/// </summary>
		/// <remarks>SPECTATOR.</remarks>
		public int? Boost { get; set; }
		/// <summary>
		/// <see langword="True"/> if the player is currently boosting.
		/// </summary>
		/// <remarks>SPECTATOR.</remarks>
		public bool? bBoosting { get; set; }
		/// <summary>
		/// <see langword="True"/> if at least 3 wheels are touching the world.
		/// </summary>
		/// <remarks>SPECTATOR.</remarks>
		public bool? bOnGround { get; set; }
		/// <summary>
		/// <see langword="True"/> if the vehicle is on a wall.
		/// </summary>
		/// <remarks>SPECTATOR.</remarks>
		public bool? bOnWall { get; set; }
		/// <summary>
		/// <see langword="True"/> if the player is holding handbrake.
		/// </summary>
		/// <remarks>SPECTATOR.</remarks>
		public bool? bPowersliding { get; set; }
		/// <summary>
		/// <see langword="True"/> if the vehicle is currently destroyed.
		/// </summary>
		/// <remarks>SPECTATOR.</remarks>
		[MemberNotNullWhen(true, nameof(Attacker))]
		public bool? bDemolished { get; set; }
		/// <summary>
		/// The player who demolished this player. Present only when demolished.
		/// </summary>
		/// <remarks>CONDITIONAL (<see cref="bDemolished"/> is <see langword="True"/>).</remarks>
		public PlayerShortcut? Attacker { get; set; }
		/// <summary>
		/// <see langword="True"/> if the vehicle is at supersonic speed.
		/// </summary>
		/// <remarks>SPECTATOR.</remarks>
		public bool? bSupersonic { get; set; }
	}
}

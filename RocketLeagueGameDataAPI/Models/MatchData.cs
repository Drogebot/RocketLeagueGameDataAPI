using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Models
{
	[JsonSerializable(typeof(MatchData))]
	public class MatchData
	{
		/// <summary>
		/// One entry per team, ordered by TeamNum.
		/// </summary>
		public required TeamData[] Teams { get; set; }
		/// <summary>
		/// Seconds remaining in the match.
		/// </summary>
		public required int TimeSeconds { get; set; }
		/// <summary>
		/// <see langword="True"/> if the match is in overtime.
		/// </summary>
		public required bool bOvertime { get; set; }
		/// <summary>
		/// Current frame number if a replay is active.
		/// </summary>
		/// <remarks>CONDITIONAL (<see cref="bReplay"/> is <see langword="True"/>).</remarks>
		public int? Frame { get; set; }
		/// <summary>
		/// Seconds elapsed since game start if a replay is active.
		/// </summary>
		/// <remarks>CONDITIONAL (<see cref="bReplay"/> is <see langword="True"/>).</remarks>
		public float? Elapsed { get; set; }
		/// <summary>
		/// Current ball state.
		/// </summary>
		public required BallData Ball { get; set; }
		/// <summary>
		/// <see langword="True"/> if a goal replay or history replay is active.
		/// </summary>
		[MemberNotNullWhen(true, [nameof(Frame), nameof(Elapsed)])]
		public required bool bReplay { get; set; }
		/// <summary>
		/// <see langword="True"/> if a team has won.
		/// </summary>
		public required bool bHasWinner { get; set; }
		/// <summary>
		/// Name of the winning team. Empty string if no winner yet.
		/// </summary>
		public required string Winner { get; set; } = string.Empty;
		/// <summary>
		/// Asset name of the current map (e.g. "Stadium_P").
		/// </summary>
		public required string Arena { get; set; }
		/// <summary>
		/// <see langword="True"/> if the client is currently viewing a specific vehicle.
		/// </summary>
		[MemberNotNullWhen(true, nameof(Target))]
		public required bool bHasTarget { get; set; }
		/// <summary>
		/// Player currently being viewed. Members are an empty string or 0 if the player does not have a spectator target.
		/// </summary>
		/// <remarks>CONDITIONAL (<see cref="bHasTarget"/> is <see langword="True"/>).</remarks>
		public PlayerShortcut? Target { get; set; }
	}
}

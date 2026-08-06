using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Commands
{
	/// <summary>
	/// Changes the spectator/replay viewpoint. At least one of Focus or Perspective must be supplied; both may be combined.
	/// </summary>
	/// <remarks><para>SPECTATOR.</para>REPLAY.</remarks>
	[JsonSerializable(typeof(Command_ChangePOV))]
	public class Command_ChangePOV : CommandData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override CommandType CommandType => CommandType.ChangePOV;
		/// <summary>
		/// What to look at — the ball or a specific player. Limited values (see below).
		/// </summary>
		/// <remarks><para>ONE OF.</para>
		/// Accepted values:<list type="bullet">
		///		<item><term>"ball"</term><description>Focus the ball.</description></item>
		///		<item><term>"&lt;shortcut&gt;"</term><description>A player's spectator shortcut number as a string (e.g. "1", "2"). Must be all digits; any other value is rejected.</description></item>
		/// </list></remarks>
		public string? Focus { get; set; }
		/// <summary>
		/// Camera mode to switch to. Limited values (see below).
		/// </summary>
		/// <remarks>ONE OF.</remarks>
		public PerspectiveType? Perspective { get; set; }
	}

	public enum PerspectiveType
	{
		/// <summary>Third-person camera behind the ball or player.</summary>
		Fly,
		/// <summary>Follow the position of a player or ball.</summary>
		SoftAttach,
		/// <summary>Follow the position and rotation of a player.</summary>
		HardAttach,
		/// <summary>View from a player's perspective. Applied automatically when Focus targets a player and no Perspective is given.</summary>
		PlayerView,
		/// <summary>Automatically spectates.</summary>
		AutoCam,
		/// <summary>Automatically switches between player views.</summary>
		Camera_Director,
	}
}

using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Commands
{
	/// <summary>
	/// Shows or hides the in-game HUD.
	/// </summary>
	[JsonSerializable(typeof(Command_SetHUDVisibility))]
	public class Command_SetHUDVisibility : CommandData
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public override CommandType CommandType => CommandType.SetHUDVisibility;
		/// <summary>
		/// <see langword="true"/> shows the HUD; <see langword="false"/> hides it.
		/// </summary>
		public required bool bVisible { get; set; }
	}
}

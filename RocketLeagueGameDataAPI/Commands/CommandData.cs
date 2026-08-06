using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI.Commands
{
	/// <summary>
	/// Base command class for all commands.
	/// </summary>
	[JsonSerializable(typeof(CommandData))]
	public abstract class CommandData
	{
		/// <summary>
		/// The <see cref="CommandType"/> of this command.
		/// </summary>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		public abstract CommandType CommandType { get; }
	}
}

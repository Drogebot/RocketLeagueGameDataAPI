using RocketLeagueGameDataAPI.Commands;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RocketLeagueGameDataAPI
{
	[JsonSerializable(typeof(CommandMessage))]
	internal class CommandMessage
	{
		public required CommandType Command { get; set; }
		public required string Data { get; set; }

		public static CommandMessage CreateCommandMessage(CommandData command, JsonSerializerOptions? options = null)
		{
			using var stream = new MemoryStream();
			switch (command.CommandType)
			{
				case CommandType.ChangePOV:
					JsonSerializer.Serialize(stream, command, typeof(Command_ChangePOV), options);
					break;
				case CommandType.LoadReplay:
					JsonSerializer.Serialize(stream, command, typeof(Command_LoadReplay), options);
					break;
				case CommandType.SeekReplay:
					JsonSerializer.Serialize(stream, command, typeof(Command_SeekReplay), options);
					break;
				case CommandType.SetGameSpeed:
					JsonSerializer.Serialize(stream, command, typeof(Command_SetGameSpeed), options);
					break;
				case CommandType.SetHUDVisibility:
					JsonSerializer.Serialize(stream, command, typeof(Command_SetHUDVisibility), options);
					break;
				case CommandType.SetMatchPaused:
					JsonSerializer.Serialize(stream, command, typeof(Command_SetMatchPaused), options);
					break;
				default:
					throw new UnknownCommandException($"Unexpected Command {command}");
			}

			return new CommandMessage()
			{
				Command = command.CommandType,
				Data = Encoding.UTF8.GetString(stream.ToArray()),
			};
		}

		//public static async ValueTask<CommandMessage> CreateCommandMessageAsync(CommandData command, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
		//{
		//	using var stream = new MemoryStream();
		//
		//	switch (command.CommandType)
		//	{
		//		case CommandType.ChangePOV:
		//			await JsonSerializer.SerializeAsync(stream, command, typeof(Command_ChangePOV), options, cancellationToken);
		//			break;
		//		case CommandType.LoadReplay:
		//			await JsonSerializer.SerializeAsync(stream, command, typeof(Command_LoadReplay), options, cancellationToken);
		//			break;
		//		case CommandType.SeekReplay:
		//			await JsonSerializer.SerializeAsync(stream, command, typeof(Command_SeekReplay), options, cancellationToken);
		//			break;
		//		case CommandType.SetGameSpeed:
		//			await JsonSerializer.SerializeAsync(stream, command, typeof(Command_SetGameSpeed), options, cancellationToken);
		//			break;
		//		case CommandType.SetHUDVisibility:
		//			await JsonSerializer.SerializeAsync(stream, command, typeof(Command_SetHUDVisibility), options, cancellationToken);
		//			break;
		//		case CommandType.SetMatchPaused:
		//			await JsonSerializer.SerializeAsync(stream, command, typeof(Command_SetMatchPaused), options, cancellationToken);
		//			break;
		//		default:
		//			throw new UnknownCommandException($"Unexpected Command {command}");
		//	}
		//
		//	return new CommandMessage
		//	{
		//		Command = command.CommandType,
		//		Data = Encoding.UTF8.GetString(stream.ToArray())
		//	};
		//}
	}

	public enum CommandType
	{
		/// <summary>Changes the spectator/replay viewpoint. At least one of Focus or Perspective must be supplied; both may be combined.</summary>
		ChangePOV,
		/// <summary>Loads and begins playback of a replay by file name or by file path.</summary>
		LoadReplay,
		/// <summary>Jumps replay playback to a target frame or time.</summary>
		SeekReplay,
		/// <summary>Sets replay playback speed.</summary>
		SetGameSpeed,
		/// <summary>Shows or hides the in-game HUD.</summary>
		SetHUDVisibility,
		/// <summary>Pauses or unpauses the current match or replay.</summary>
		SetMatchPaused,
	}
}
